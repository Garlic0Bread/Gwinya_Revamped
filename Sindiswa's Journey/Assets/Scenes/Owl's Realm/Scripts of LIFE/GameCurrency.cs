using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameCurrency : MonoBehaviour
{
    [SerializeField] private int Points; //core is received from shooting enemies <when bullet touches enemye>
    [SerializeField] private TMP_Text numCoins;

    [SerializeField] private Image expBar;
    [SerializeField] private int exp;


    private void Update()
    {
        numCoins.text = Points.ToString();
        if (exp == 100)
        {
           // Ability_Spawner spawnItem = FindObjectOfType<Ability_Spawner>(); When player's Exp reaches 100 reward with choice of ability
           // spawnItem.Spawn();
            exp = 0;
        }
    }

    public void EarnPoints(int pointsEarned) //call when bullet collides with enemy
    {
        Points += pointsEarned;
        Debug.Log(Points);
    }

    public void UpdateExp(int expToGive) //call when enemy dies
    {
        exp += expToGive;
        expBar.fillAmount = exp / 100f;
       // Enemy increaseEnemySpeed = FindObjectOfType<Enemy>();
       // increaseEnemySpeed.IncreaseSpeed(0.05f);
    }
}
