using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletforce = 20f;
    public float spreadAngle = 15f;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    public int numBinSpread = 3;

    public enum ShootMode
    {
        Single,
        Spread,
        Rapid
    }

    public ShootMode currentShootmode = ShootMode.Single;

    void Start()
    {
        SetShootMode(currentShootmode);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetShootMode(ShootMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetShootMode(ShootMode.Spread);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetShootMode(ShootMode.Rapid);
        }
    }

    void SetShootMode(ShootMode newMode)
    {
        currentShootmode = newMode;
        Debug.Log("Switched to " + currentShootmode.ToString() + " mode");
    }



    void Shooting()
    {
        switch (currentShootmode)
        {
            case ShootMode.Single:
                ShootSingle();
                break;
            case ShootMode.Spread:
                ShootSpread();
                break;
            case ShootMode.Rapid:
                ShootRapid();
                break;
        }
    }

    void ShootSingle()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(firePoint.up * bulletforce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on bullet prefab.");
        }

        Destroy(bullet, 1f);
    }

    void ShootSpread()
    {
        for (int i = 0; i < numBinSpread; i++)
        {
            float angle = firePoint.rotation.eulerAngles.z - spreadAngle / 2f + i * (spreadAngle / (numBinSpread - 1));

            // Calculate direction based on the angle
            Vector3 direction = Quaternion.Euler(0f, 0f, angle) * Vector3.up;

            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = direction * bulletforce;
            }
            else
            {
                Debug.LogError("Rigidbody2D not found on bullet prefab.");
            }

            Destroy(bullet, 2f);
        }
    }

    void ShootRapid()
    {
        if (Time.time > nextFireTime)
        {
            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = firePoint.up * bulletforce;
            }
            else
            {
                Debug.LogError("Rigidbody2D not found on bullet prefab.");
            }

            Destroy(bullet, 2f);

            nextFireTime = Time.time + fireRate; // Set the next allowed fire time
        }




    }
}
    

