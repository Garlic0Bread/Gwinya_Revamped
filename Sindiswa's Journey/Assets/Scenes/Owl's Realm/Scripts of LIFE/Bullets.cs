using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    [SerializeField] private float bulletLife;
    [SerializeField] private int bulletDamage;
    [SerializeField] private int damagePoint;
    [SerializeField] private string bulletTypeString;
    public float radius = 2.0f; // Adjust the radius as needed
    public LayerMask detectionLayer; // Set this in the Inspector to specify which layers the circle should detect

    public bool kirinActive = false;

    private GameObject player;
    [SerializeField] private GameObject kirin;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        Destroy(gameObject, bulletLife);
    }
    private void FixedUpdate()
    {
        BulletType(bulletTypeString);

    }

    private void OnDrawGizmos()
    {
        // Draw the detection circle in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // types of bullets
    private void BulletType(string bulletType)
    {
        if (bulletTypeString == "Homing Bullet") //homing bullet
        {
            Vector3 targetPos = player.transform.position;
            float speed = followSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);
        }

        else if(bulletTypeString == "Kirin")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, detectionLayer);
            foreach (Collider2D col in colliders)
            {
                print("kirin");
                Destroy(gameObject, 2);

                col.GetComponent<Health>().Damage(bulletDamage);
                Instantiate(kirin, col.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health dealDamage = collision.GetComponent<Health>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, detectionLayer);

        if (collision.gameObject.layer != 6 && dealDamage != null)
        {
            dealDamage.Damage(bulletDamage);
            Destroy(gameObject);

            GameCurrency addPoint = FindObjectOfType<GameCurrency>();
            addPoint.EarnPoints(damagePoint);
            
        }
    }
}  
