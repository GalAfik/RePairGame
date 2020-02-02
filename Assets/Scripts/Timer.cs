using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining; // How much time is left in the round?
    public Text timerDisplay; // What UI Text element will be used to display the timer?
    public bool activeCountdown; // Is the timer currently counting down?

    private float maxTime = 20; // The max amount of time for the timer
    private int initialFontSize;
    private Vector3 initialFontPosition;


    private void Start()
    {
        activeCountdown = false;
        initialFontSize = timerDisplay.fontSize;
        initialFontPosition = timerDisplay.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Only count down if the timer is active
        if (activeCountdown)
        {
            // Decrement the timer
            timeRemaining -= Time.deltaTime;

            // Increase the size of the timer display and move it down a bit
            timerDisplay.transform.localScale += new Vector3(.0004f, .0004f, 0f);
            timerDisplay.transform.position -= new Vector3(0f, .01f, 0f);
        }

        // Display the timer on the UI
        timerDisplay.text = timeRemaining.ToString("0.00");

        // Check if the timer is completely elapsed
        if (timeRemaining <= 0)
        {
            // Stop the countdown
            activeCountdown = false;
            // Set the timer to 0 to make sure it doesn't display a negative number
            timeRemaining = 0;
            // Change the display to show only "0"
            timerDisplay.text = "0";
            timerDisplay.fontSize = 200;
            timerDisplay.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
            timerDisplay.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 500);
            timerDisplay.transform.position = new Vector3(
                (Screen.width / 2)
                , (Screen.height / 2)
                , 0);
        }
    }

    // Reset the timer back to its starting state
    public void Reset()
    {
        timeRemaining = maxTime;
        activeCountdown = false;
        timerDisplay.transform.localScale = new Vector3(1, 1, 0);
        timerDisplay.transform.position = new Vector3(0f, 0f, 0f);
        timerDisplay.fontSize = initialFontSize;
        timerDisplay.transform.position = initialFontPosition;
        timerDisplay.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        timerDisplay.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
    }
}
