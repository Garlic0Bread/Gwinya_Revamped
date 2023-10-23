using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxhealth = 100;
    [SerializeField] private int enemy_ExpWorth;

    [SerializeField] private GameObject pinkyDeathVFX;
    [SerializeField] private GameObject Gwinya;
    [SerializeField] private Image healthBar;

    private void Update()
    {
        //if (this.CompareTag("Player"))
        {
            //healthBar.fillAmount = health / 100f;
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

        if (health < 0)
        {
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
        Destroy(gameObject);
        if (this.CompareTag("Enemy"))
        {
            GameCurrency addExp = FindObjectOfType<GameCurrency>();
            addExp.UpdateExp(enemy_ExpWorth);
            Instantiate(pinkyDeathVFX, transform.position, Quaternion.identity);
        }

        else if (this.CompareTag("Player"))
        {
            //eneManager.LoadScene(2);
        }
    }

    private IEnumerator visualIndicator(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
