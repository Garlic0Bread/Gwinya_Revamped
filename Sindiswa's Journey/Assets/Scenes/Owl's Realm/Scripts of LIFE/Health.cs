using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    [SerializeField] private float maxhealth = 100;
    [SerializeField] private float enemy_ExpWorth;
    [SerializeField] private int enemyDeadPoint;

    [SerializeField] private GameObject DeathVFX;
    [SerializeField] private GameObject Gwinya;
    [SerializeField] private Image healthBar;

    [SerializeField] private AudioSource Death;

    private void Update()
    {
        if (this.CompareTag("Player"))
        {
            healthBar.fillAmount = health / 100f;
        }
    }
    public void Damage(float amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;
        StartCoroutine(visualIndicator(Color.red));

        if (health <= 0)
        {
            Death.Play();
            Die();
        }
    }
    public void Heal(float amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldBeOverMaHealth = health + amount > maxhealth;
        StartCoroutine(visualIndicator(Color.green));
        if (wouldBeOverMaHealth)
        {
            this.health = maxhealth;
        }

        else
        {
            this.health += amount;
        }
    }
    private void Die()
    {
        if (this.CompareTag("Enemy"))
        {
            GameCurrency updateCurrencies = FindObjectOfType<GameCurrency>();
            updateCurrencies.EnemiesLeft(enemyDeadPoint);
            updateCurrencies.IncreaseExp(enemy_ExpWorth);
            Instantiate(DeathVFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private IEnumerator visualIndicator(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
