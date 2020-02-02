using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMaster : MonoBehaviour
{
    public GameObject[] players; // Take in the player objects to disable them
    public GameObject thankYouCanvas; // The canvas to be displayed at the end of the game
    public GameObject timerDisplay; // The timer for the game needs to be removed when the credits play

    private GameObject thankYouMessageImage; // The credits image to be displayed and animated at the end of the game

    private void Start()
    {
        // Hide all elements of the canvas that relate to the credits
        thankYouMessageImage = thankYouCanvas.transform.Find("ThankYouMessage").gameObject;
        thankYouMessageImage.GetComponent<Animator>().enabled = false;
        thankYouMessageImage.SetActive(false);
        // Deactivate the panel
        thankYouCanvas.transform.Find("Panel").gameObject.SetActive(false);
    }

    public void StartCredits()
    {
        // Hide the game timer display
        timerDisplay.SetActive(false);

        foreach (GameObject player in players)
        {
            // Take player control away for the credits duration
            player.GetComponent<PlayerObject>().hasControl = false;

            // Display the hearts particles
            player.transform.Find("Hearts").GetComponent<ParticleSystem>().Play();
            ParticleSystem.EmissionModule heartsEmitter = player.transform.Find("Hearts").GetComponent<ParticleSystem>().emission;
            heartsEmitter.enabled = true;
        }

        // Display the credits image
        thankYouCanvas.transform.Find("Panel").gameObject.SetActive(true);
        thankYouMessageImage.SetActive(true);
        thankYouMessageImage.GetComponent<Animator>().enabled = true;
    }
}
