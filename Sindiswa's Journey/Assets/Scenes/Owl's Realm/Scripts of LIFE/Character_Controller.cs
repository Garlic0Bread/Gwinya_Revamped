using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float playerOriginalSpeed;
    [SerializeField] private float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] private float lockOnRange = 10f; // Range within which enemies can be locked onto
    [SerializeField] private float fireRate = 0.5f; // Rate of fire (in seconds)
    [SerializeField] private float nextFireTime; // Time of the next allowed fire
    [SerializeField] private float playerSpeed;

    [SerializeField] private Transform playerGun; // Reference to the player's gun transform
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet to be spawned
    [SerializeField] private Joystick_Movement joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void PlayerMove()
    {
        if (joystick.joystick_Vector.y != 0)
        {
            rb.velocity = new Vector2(joystick.joystick_Vector.x * playerSpeed, joystick.joystick_Vector.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        //run to the right
        if (joystick.joystick_Vector.x > 0.35f) 
        {
            rb.velocity = new Vector2(joystick.joystick_Vector.x * playerSpeed, joystick.joystick_Vector.y * playerSpeed);
            //animator.SetBool("rightRun", true);
        }
        else if (joystick.joystick_Vector.x <= 0.35f)
        {
            //animator.SetBool("rightRun", false);
        }

        if (joystick.joystick_Vector.x < -0.35f) //run to the left
        {
            rb.velocity = new Vector2(joystick.joystick_Vector.x * playerSpeed, joystick.joystick_Vector.y * playerSpeed);
            //animator.SetBool("leftRun", true);
        }
        else if (joystick.joystick_Vector.x >= -0.35f)
        {
            //animator.SetBool("leftRun", false);
        }
    }
    void FireBullet()
    {
        // Instantiate a bullet prefab at the player's gun position and rotation
        GameObject bullet = Instantiate(bulletPrefab, playerGun.position, Quaternion.identity);

        // Apply velocity to the bullet in the forward direction of the gun
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = playerGun.forward * bulletSpeed;
    }
    void FindEnemiesInScene() //lock on to enemies and shoot at them
    {
        GameObject[] enemies = FindObjectsOfType<GameObject>().Where(go => ((3 << go.layer) & targetLayer.value) != 0).ToArray();
        if (enemies.Length > 0)
        {
            // Find the closest enemy
            Transform closestEnemy = GetClosestEnemy(enemies);

            // Check if the closest enemy is within lock-on range
            if (Vector3.Distance(transform.position, closestEnemy.position) <= lockOnRange)
            {
                // Rotate the player's gun to face the closest enemy
                Vector3 targetDirection = closestEnemy.position - playerGun.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                playerGun.rotation = Quaternion.Lerp(playerGun.rotation, targetRotation, Time.deltaTime * 10f);

                // Check if enough time has passed since the last fire

                if (Time.time >= nextFireTime)
                {
                    // Fire a bullet
                    FireBullet();
                    // Set the time for the next allowed fire
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    Transform GetClosestEnemy(GameObject[] enemies) //find enemies that are within range and lock on to them
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    } 

    void FixedUpdate()
    {
        PlayerMove();
        FindEnemiesInScene();
    }
}
