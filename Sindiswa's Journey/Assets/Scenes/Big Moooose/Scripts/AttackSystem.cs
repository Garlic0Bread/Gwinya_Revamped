using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public float attackRadius = 15f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);

            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    // You can damage or destroy the enemy here.
                    Destroy(col.gameObject);
                }
            }
        }
    }
}
