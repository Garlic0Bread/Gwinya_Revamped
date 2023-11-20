using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    public Animator Anim, Anim1, Anim2;

    public GameObject Lover1;

    public void Start()
    {
        Lover1.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            Anim.Play("Male");
            Debug.Log("Works");
            Lover1.SetActive(true);
        }
    }
}
