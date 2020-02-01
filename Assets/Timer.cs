using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining; // How much time is left in the round?
    public Text timerDisplay; // What UI Text element will be used to display the timer?
    private bool active; // Is the timer currently counting down?

    private void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Only count down if the timer is active
        if (active)
        {
            // Decrement the timer
            timeRemaining -= Time.deltaTime;
        }

        // Display the timer on the UI
        timerDisplay.text = timeRemaining.ToString("0.00");

        // Check if the timer is completely elapsed
        if (timeRemaining <= 0)
        {
            // Stop the countdown
            active = false;
            // Set the timer to 0 to make sure it doesn't display a negative number
            timeRemaining = 0;
        }
    }
}
