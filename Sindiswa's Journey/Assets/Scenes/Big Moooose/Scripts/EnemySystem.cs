using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public float radius = 2.0f;
    public LayerMask detectionLayer;
    public float damageAmount = 50;
    public KeyCode damageKey = KeyCode.Space;

    private void OnDrawGizmos()
    {
        // Draw the detection circle in the Scene view
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, detectionLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy")) // Change "Enemy" to your actual tag or layer name
            {
                // Apply damage to the enemy
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    float actualDamage = 50;
                    if (collider.gameObject != gameObject) // Ensure the damaged enemy itself is not affected
                    {
                        actualDamage /= 2.0f; // Half the damage for nearby enemies
                        enemyHealth.TakeDamage(actualDamage);
                    }
                }
            }
        }
    }
}
