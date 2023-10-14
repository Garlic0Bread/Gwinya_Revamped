using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private int bulletDamage;
    [SerializeField] private int damagePoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<Health>() != null) //accounts for player's Bullet
        {
            print("damaging enemy");
            //damage the enemy gameObject if it has the Health script
            Health damageEnemy = collision.gameObject.GetComponent<Health>();
            damageEnemy.Damage(bulletDamage);

            //increase the player's points everytime the shoot at an enemy
            GameCurrency addPoint = FindObjectOfType<GameCurrency>();
            addPoint.EarnPoints(damagePoint);
            collision.gameObject.GetComponent<Health>();
        }
    }

    // types of bullets
    private void HomingBullet()
    {

    }

    private void NormalBullet()
    {

    }

    private void LaserBullet()
    {

    }
}
