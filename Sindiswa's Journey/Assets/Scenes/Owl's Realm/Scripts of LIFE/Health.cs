using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxhealth = 100;
    [SerializeField] private int tokolosh_Exp;
    [SerializeField] private int pinky_Exp;

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
    public void Damage(int amount)
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
    public void Heal(int amount)
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
            Instantiate(pinkyDeathVFX, transform.position, Quaternion.identity);
            Instantiate(Gwinya, transform.position, Quaternion.identity);

            if (this.gameObject.layer == 3)
            {
               //oinManger addExp = FindObjectOfType<CoinManger>();
                // (addExp != null)
                {
                    //dExp.UpdateExp(tokolosh_Exp);
                }
            }
            else if (this.gameObject.layer == 6)
            {
                //inManger addExp = FindObjectOfType<CoinManger>();
               //f (addExp != null)
                {
                    //dExp.UpdateExp(pinky_Exp);
                }
            }
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
