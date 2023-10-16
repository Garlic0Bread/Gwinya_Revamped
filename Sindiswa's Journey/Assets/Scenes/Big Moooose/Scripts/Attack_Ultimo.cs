using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Ultimo : MonoBehaviour
{
    public float attackRadius = 15f;

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses a button to attack (e.g., left mouse button).
        if (Input.GetButtonDown("Fire1"))
        {
            // Find all GameObjects with the "enemy" tag within the specified radius.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);

            // Loop through the colliders and check if they have the "enemy" tag.
            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    // You can damage or destroy the enemy here.
                    Destroy(col.gameObject);
                    Debug.Log("Works");
                }
            }
        }
    }
}
