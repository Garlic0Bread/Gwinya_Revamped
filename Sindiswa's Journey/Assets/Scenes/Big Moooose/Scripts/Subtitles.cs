using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    public float activationTime = 5f; // Adjust this time as needed
    public Text textToActivate;

    private float timer;
    private bool textActivated = false;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has reached the activation time and the text hasn't been activated yet
        if (timer >= activationTime && !textActivated)
        {
            ActivateText();
        }
    }

    void ActivateText()
    {
        // Activate the text or do any other actions you want
        textToActivate.gameObject.SetActive(true);

        // Set a flag to ensure the text is not activated again
        textActivated = true;
    }
}
