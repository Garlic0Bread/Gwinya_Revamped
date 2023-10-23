using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameCurrency : MonoBehaviour
{
    [SerializeField] private int Points; //core is received from shooting enemies <when bullet touches enemye>
    [SerializeField] private TMP_Text numCoins;

    private int numberOfObjectsToActivate = 3;
    [SerializeField] private GameObject[] abilities;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Image expBar;
    [SerializeField] private int exp;

    private void Update()
    {
        //numCoins.text = Points.ToString();
        // Ability_Spawner spawnItem = FindObjectOfType<Ability_Spawner>(); When player's Exp reaches 100 reward with choice of ability
        // spawnItem.Spawn();

        Activate_AbilitySelecter();
    }
    void Activate_AbilitySelecter()
    {
        if (exp >= 100)
        {
            exp = 0;
            SelectRandomAbilities();
        }
    }

    void SelectRandomAbilities()
    {
        if (abilities.Length <= 0)
        {
            Debug.LogWarning("No GameObjects or no objects to activate.");
            return;
        }

        int randomIndex = Random.Range(0, abilities.Length); // Choose a random index.
        abilities[randomIndex].SetActive(true);

        void Disable()
        {
            abilities[randomIndex].SetActive(false);
        }

        foreach (GameObject ability in abilities)
        {
            Button newButton = ability.GetComponentInChildren<Button>();
            newButton.onClick.AddListener(Disable);
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
