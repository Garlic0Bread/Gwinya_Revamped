using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100.0f; // Maximum health of the enemy
    public float currentHealth; // Current health of the enemy

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health doesn't go below 0 or above maxHealth

        if (currentHealth <= 0)
        {
            Die(); // Implement the Die method if needed
        }
    }

    private void Die()
    {
        // Handle enemy death here (e.g., destroy the enemy object)
        Destroy(gameObject);
    }
}
