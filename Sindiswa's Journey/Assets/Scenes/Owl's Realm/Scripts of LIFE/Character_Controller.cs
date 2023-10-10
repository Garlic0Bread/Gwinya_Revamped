using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float playerSpeed;
    [SerializeField] private Joystick_Movement joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(joystick.joystick_Vector.y != 0)
        {
            rb.velocity = new Vector2(joystick.joystick_Vector.x * playerSpeed, joystick.joystick_Vector.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
