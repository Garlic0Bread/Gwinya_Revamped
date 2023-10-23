using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float lifetime;
   
    void Update()
    {

        if (this.gameObject.CompareTag("Multiplier"))
        {
            Button multiplierUsed = gameObject.GetComponent<Button>();
            multiplierUsed.onClick.AddListener(DeactivateSelf);
        }
        else
        {
            Destroy(gameObject, lifetime);
        }
    }

    void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }
}
