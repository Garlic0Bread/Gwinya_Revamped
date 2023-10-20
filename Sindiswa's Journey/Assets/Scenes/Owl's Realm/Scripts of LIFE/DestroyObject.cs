using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float lifetime;
   
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
