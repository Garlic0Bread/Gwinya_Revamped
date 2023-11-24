using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dTrig;
    public Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
      body =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Replace "Player" with the tag you want to check
        {
            dTrig.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Replace "Player" with the tag you want to check
        {
            dTrig.SetActive(false);
        }
    }
}
