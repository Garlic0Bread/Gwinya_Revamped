using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float bulletLife;
    [SerializeField] private int bulletDamage;
    [SerializeField] private int damagePoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerFeet");
    }
    private void Update()
    {
        if (this.gameObject.CompareTag("HomingBullet"))
        {
            BulletType(1);
        }

        else
        {
            Destroy(gameObject, bulletLife);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null) //if what the bullet touches has health, deal damage to it
        {
            Health dealDamage = collision.gameObject.GetComponent<Health>();
            dealDamage.Damage(bulletDamage);

            //increase the player's points when their bullet hits an enemy
            if(collision.gameObject.layer == 3)
            {
                GameCurrency addPoint = FindObjectOfType<GameCurrency>();
                addPoint.EarnPoints(damagePoint);
            }
        }
        Destroy(gameObject);
    }

    // types of bullets
    private void BulletType(int bulletType)
    {
        if (bulletType == 1) //homing bullet
        {
            print("homin on player");
            Vector3 targetPos = player.transform.position;
            float speed = followSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPos, speed);
        }
    }
}
