using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    
    public GameObject bulletPrefab;

    public float bulletforce = 20f;


    // Start is called before the first frame update
    void Start()
    {
       




    }
        // Update is called once per frame
        void Update()
        {
           if (Input.GetButtonDown("Fire1"))
        {

            Shooting();
        }



        }

    void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
       Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse);

        Destroy(bullet, 2f);
    }


   
}
    

