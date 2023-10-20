using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float bulletLife;
    [SerializeField] private float bulletSpeed = 10f; 

    [SerializeField] private float radius = 0; 

    [SerializeField] private int bulletDamage;
    [SerializeField] private int damagePoint;

    [SerializeField] private string bulletTypeString;
    [SerializeField] private LayerMask detectionLayer; // Set this in the Inspector to specify which layers the circle should detect

    [SerializeField] private GameObject Kirin_Lightning;
    [SerializeField] private GameObject Kirin_Bullet;
    public bool kirinActive = false;
    private GameObject player;

    private void OnDrawGizmos()
    {
        // Draw the detection circle in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void FixedUpdate()
    {
        BulletType(bulletTypeString);
    }
    // types of bullets
    public void BulletType(string bulletType)
    {
        if (bulletTypeString == "Homing Bullet") //homing bullet
        {
            Vector3 targetPos = player.transform.position;
            float speed = followSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }

        else if(bulletTypeString == "Kirin") //lightning AOE move
        {
            kirinActive = true;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, detectionLayer);
            foreach (Collider2D col in colliders)
            {
                col.GetComponent<Health>().Damage(bulletDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health dealDamage = collision.GetComponent<Health>();

        if(kirinActive == true && dealDamage != null)
        {
            Instantiate(Kirin_Lightning, collision.transform.position, Quaternion.identity);
            gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            kirinActive = false;
            radius = 1.5f;
        }
        if (collision.gameObject.layer != 6 && dealDamage != null)
        {
            dealDamage.Damage(bulletDamage);
            Destroy(gameObject); //add time to make a bullet that goes through enemies

            GameCurrency addPoint = FindObjectOfType<GameCurrency>();
            addPoint.EarnPoints(damagePoint);
        }
    }
}  
