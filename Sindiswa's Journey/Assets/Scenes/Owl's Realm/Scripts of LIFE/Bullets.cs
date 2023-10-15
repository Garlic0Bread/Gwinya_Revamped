using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private Collider2D colliderToIgnore;
    [SerializeField] private float followSpeed;
    [SerializeField] private float bulletLife;
    [SerializeField] private int bulletDamage;
    [SerializeField] private int damagePoint;
    [SerializeField] private int bulletTypeNumber;

    public string currentBulletTag;
    public bool normalBullet = true;
    public int layerNumberToIgnore;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerFeet");
        colliderToIgnore = gameObject.GetComponent<Collider2D>();
    }
    private void Update()
    {
        BulletType(bulletTypeNumber);

        Destroy(gameObject, bulletLife);

    }

    // types of bullets
    private void BulletType(int bulletType)
    {
        if (bulletType == 1) //normal bullet
        {
            normalBullet = true;
        }

        else if (bulletType == 2) //homing bullet
        {
            print("homin on player");
            normalBullet = false;
            Vector3 targetPos = player.transform.position;
            float speed = followSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPos, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null && normalBullet == true) //if what the bullet touches has health, deal damage to it
        {
            if (this.gameObject.CompareTag(currentBulletTag) && collision.gameObject.layer == layerNumberToIgnore)
            {
                print("jjj");
                this.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                Health dealDamage = collision.gameObject.GetComponent<Health>();
                dealDamage.Damage(bulletDamage);
                Destroy(gameObject);
            
                if (collision.gameObject.layer == 3) //increase the player's points when their bullet hits an enemy
                {
                    GameCurrency addPoint = FindObjectOfType<GameCurrency>();
                    addPoint.EarnPoints(damagePoint);
                }
            }
        }
    }
    

}
