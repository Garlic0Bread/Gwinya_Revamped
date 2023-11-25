using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private int damagePoint;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float attackCooldown;

    [SerializeField] private float nextFireTime; // Time of the next allowed fire
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float lockOnRange = 10f; 
    [SerializeField] private float bulletSpeed = 1.5f;

    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject shrapnelVFX;
    [SerializeField] private Transform bulletSpawnPoint;

    private float _lastAttackTime;
    private GameObject Player;
    private Rigidbody2D playerRB;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        Vector2 lookDirection = Player.GetComponent<Rigidbody2D>().position - playerRB.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f; //calculates the angle to adjust the body towards the mosue position.
        playerRB.rotation = angle;

        if (player.Length > 0)
        {
            // Find the closest enemy
            Transform closestPlayer = GetClosestPlayer(player);

            // Check if the closest enemy is within lock-on range
            if (gameObject.layer == 6 && (transform.position - closestPlayer.position).sqrMagnitude <= lockOnRange * lockOnRange) //moving enemies, layer
            {
                Swarm();
            }

            else if (gameObject.layer == 7 && (transform.position - closestPlayer.position).sqrMagnitude <= lockOnRange) //shooting layer
            {
                if (Time.time >= nextFireTime)
                {
                    ShootPlayer();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    Transform GetClosestPlayer(GameObject[] players)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = (transform.position - player.transform.position).sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = player.transform;
            }
        }
        return closestEnemy;
    }

    private void ShootPlayer()
    {
        GameObject bullet = Instantiate(enemyBullet, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bulletSpawnPoint.up * bulletSpeed;
    }
    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    public void IncreaseSpeed(float increaseBy)
    {
        speed += increaseBy;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Time.time - _lastAttackTime < attackCooldown) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Health dealDamage = collision.gameObject.GetComponent<Health>();
            dealDamage.Damage(damage);
            _lastAttackTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerBullet"))
        {
            Instantiate(shrapnelVFX, transform.position, Quaternion.identity);
        }
    }
}
