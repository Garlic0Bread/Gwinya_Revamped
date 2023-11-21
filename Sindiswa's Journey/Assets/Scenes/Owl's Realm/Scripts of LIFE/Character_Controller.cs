using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [SerializeField] private float playerOriginalSpeed;
    [SerializeField] private float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] private float lockOnRange = 10f; // Range within which enemies can be locked onto
    [SerializeField] private float fireRate = 0.5f; // Rate of fire (in seconds)
    [SerializeField] private float nextFireTime; // Time of the next allowed fire
    [SerializeField] private float shieldTimout;
    [SerializeField] private float playerSpeed;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject kirinBullet; 
    [SerializeField] private GameObject shield;


    private Rigidbody2D rb;
    private GameCurrency gameCurrency;
    private Vector2 mousePos; //Vector for the mouse position
    private Vector2 movement;

    [SerializeField] private Transform playerGun;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Joystick_Movement joystick;

    [SerializeField] private Camera cam;
    [SerializeField] private Button KirinButton;
    [SerializeField] private bool canFire = false;
    [SerializeField] private bool mobileController;
    [SerializeField] private bool Kirin_Active = false;
    [SerializeField] private bool Shield_Active = false;

    void Start()
    {
        gameCurrency = FindObjectOfType<GameCurrency>();
        rb = GetComponent<Rigidbody2D>();
        shield.SetActive(false);
        Shield_Active = false;
    }
    private void Update()
    {
        Vector2 lookDir = joystick.joystick_Vector;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        movement.y = Mathf.Lerp(movement.y, Input.GetAxis("Vertical"), 0.2f); //Gets axis for vertical input.
        movement.x = Mathf.Lerp(movement.x, Input.GetAxis("Horizontal"), 0.2f);  //Gets axis for horizontal input.
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //makes the mouse position relative to the screen in game.

        if (Shield_Active == true)
        {
            shieldTimout -= Time.deltaTime;
        }
        FireBullet();
    }
    void FixedUpdate()
    {
        PlayerMove();
        FindEnemiesInScene();
    }

    void PlayerMove()
    {
        if(mobileController == true)
        {
            if (joystick.joystick_Vector.y != 0)
            {
                rb.velocity = new Vector2(joystick.joystick_Vector.x * playerSpeed, joystick.joystick_Vector.y * playerSpeed);
                canFire = false;
            }
            else
            {
                rb.velocity = Vector2.zero;
                canFire = true;
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
        else
        {
            rb.MovePosition(rb.position + movement * playerSpeed * Time.deltaTime); //rotates the rigidbody with relation to the mouse.
            Vector2 lookDirection = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f; //calculates the angle to adjust the body towards the mosue position.
            rb.rotation = angle;
        }
        
    }
    void FireBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate a bullet prefab at the player's gun position and rotation
            GameObject bullet = Instantiate(bulletPrefab, playerGun.position, Quaternion.identity);

            // Apply velocity to the bullet in the forward direction of the gun
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = playerGun.up * bulletSpeed;
        }

        if (Kirin_Active == true)
        {
            GameObject kirin = Instantiate(kirinBullet, playerGun.position, Quaternion.identity);
            Rigidbody2D KirinRigidbody = kirin.GetComponent<Rigidbody2D>();
            KirinRigidbody.velocity = playerGun.forward * bulletSpeed;
            Kirin_Active = false;
        }

    }
    void FindEnemiesInScene() //lock on to enemies and shoot at them
    {
        GameObject[] enemies = FindObjectsOfType<GameObject>().Where(go => ((3 << go.layer) & targetLayer.value) != 0).ToArray();
        if (enemies.Length > 0)
        {
            // Find the closest enemy
            Transform closestEnemy = GetClosestEnemy(enemies);

            // Check if the closest enemy is within lock-on range
            if (Vector3.Distance(transform.position, closestEnemy.position) <= lockOnRange && canFire == true)
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

    public void Kirin()
    {
        Kirin_Active = true;
    }
    public void Increase_FireRate(float IncreaseAmount)
    {
        if(gameCurrency.Points <= 0)
        {
            return;
        }
        else
        {
            fireRate -= IncreaseAmount;
            gameCurrency.Points -= 1;
        }
    }
    public void ActivateShield()
    {
        if (gameCurrency.Points <= 0)
        {
            return;
        }
        else
        {
            StartCoroutine(ShieldDuration());
            gameCurrency.Points -= 3;
        }
    }
    public void Increase_Speed(float IncreaseAmount)
    {
        if (gameCurrency.Points <= 0)
        {
            return;
        }
        else
        {
            playerSpeed += IncreaseAmount;
            gameCurrency.Points -= 2;
        }
    }

    IEnumerator ShieldDuration()
    {
        shield.SetActive(true);
        Shield_Active = true;
        yield return new WaitForSeconds(shieldTimout);
        Shield_Active = false;
        shield.SetActive(false);
    }
}
