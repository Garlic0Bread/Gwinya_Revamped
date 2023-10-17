using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionScript : MonoBehaviour
{
    public float radius = 2.0f; // Adjust the radius as needed
    public LayerMask detectionLayer; // Set this in the Inspector to specify which layers the circle should detect
    public KeyCode damageKey = KeyCode.Space;
    public float damageAmount = 10;

    private void OnDrawGizmos()
    {
        // Draw the detection circle in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Update()
    {
        // Detect objects within the circle's radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, detectionLayer);

        foreach (Collider2D collider in colliders)
        {
            if (Input.GetKeyDown(damageKey))
            {
                Debug.Log("Detected: " + collider.name);

                if (collider.CompareTag("Enemy")) // Change "Enemy" to your actual tag or layer name
                {
                    // Apply damage to the enemy
                    EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        float actualDamage = damageAmount;
                        if (collider.gameObject != gameObject) // Ensure the damaged enemy itself is not affected
                        {
                            actualDamage /= 2.0f; // Half the damage for nearby enemies
                        }
                        enemyHealth.TakeDamage(damageAmount);
                    }
                }
            }
        }
    }
}
