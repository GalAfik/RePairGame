using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public new GameObject camera; // The Main Camera for the scene
    public GameObject[] players; // The master player objects
    public GameObject[] levels; // This is the master list of level prefabs for the game
    public GameObject logoImage; // The logo to be displayed on the main menu and between levels
    public GameObject[] countdownImages; // The countdown raw images to be displayed during the level start
    public GameObject transitionEffect; // The transition effect to display between levels
    public GameObject timer; // The timer object used for keeping time in game
    public int levelIndex; // The current level's index in the levels array
    public GameObject creditsMasterObject; // The master object that controls the game's credits

    private GameObject currentLevelObject; // The currently loaded level object

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        // Reset the transition images before the first level starts
        ResetTransitionImages();

        // Display a "prepare to re-pair" message
        logoImage.SetActive(true);
        logoImage.GetComponent<Animator>().enabled = true;

        // Display a transition effect
        transitionEffect.SetActive(true);
        transitionEffect.GetComponent<Animator>().enabled = true;

        yield return new WaitForSecondsRealtime(0.7f);

        // Load the level
        LoadLevel();
    }

    private void Update()
    {
        // When the time runs out, the players should lose
        if (timer != null && timer.GetComponent<Timer>().timeRemaining == 0)
        {
            // Start the lose level sequence
            StartCoroutine(LoseLevel());
        }
    }

    // Prepare the level objects
    private void LoadLevel()
    {
        if (levelIndex < levels.Length)
        {
            // Destroy the old level
            Destroy(currentLevelObject);

            // Get the level prefab and install it into the game world
            currentLevelObject = Instantiate(levels[levelIndex], Vector3.zero, new Quaternion());

            // Snap the players to their new positions
            // Player 1
            Vector3 player1SnapPosition = currentLevelObject.transform.Find("Player1Snap").position;
            Quaternion player1SnapRotation = currentLevelObject.transform.Find("Player1Snap").rotation;
            players[0].transform.position = player1SnapPosition;
            players[0].transform.rotation = player1SnapRotation;
            // Player 2
            Vector3 player2SnapPosition = currentLevelObject.transform.Find("Player2Snap").position;
            Quaternion player2SnapRotation = currentLevelObject.transform.Find("Player2Snap").rotation;
            players[1].transform.position = player2SnapPosition;
            players[1].transform.rotation = player2SnapRotation;

            // Reset player's state
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerObject>().ResetPlayer();
            }

            // Snap the camera to its new position
            Vector3 cameraSnapPosition = currentLevelObject.transform.Find("CameraSnap").position;
            Quaternion cameraSnapRotation = currentLevelObject.transform.Find("CameraSnap").rotation;
            camera.transform.position = cameraSnapPosition;
            camera.transform.rotation = cameraSnapRotation;
            camera.GetComponent<CameraShake>().Reset();
        }

        if (levelIndex != levels.Length - 1)
        {
            // Disable player control
            SetPlayerControl(false);

            // Reset the timer
            timer.GetComponent<Timer>().Reset();

            // Reset the countdown images
            ResetCountdownImages();
            StartCoroutine(StartLevelCountdown());
        }
        else
        {
            creditsMasterObject.GetComponent<CreditsMaster>().StartCredits();
        }
    }

    // Play as the level starts to begin the countdown and give players control over their player objects
    IEnumerator StartLevelCountdown()
    {
        // Count down
        for (int i = 0; i < countdownImages.Length; i++)
        {
            // Display the countdown images before starting the level
            countdownImages[i].SetActive(true);
            countdownImages[i].GetComponent<Animator>().enabled = true;

            // Enable player control on the "Re-pair!" message
            if (i == countdownImages.Length - 1)
            {
                // Start the timer countdown
                if (timer != null) timer.GetComponent<Timer>().activeCountdown = true;
                SetPlayerControl(true);
            }

            // Wait for one second between each countdown image
            yield return new WaitForSecondsRealtime(1);
        }

        ResetTransitionImages();
    }

    public IEnumerator WinLevel()
    {
        // Stop the timer countdown
        timer.GetComponent<Timer>().activeCountdown = false;
        yield return new WaitForSecondsRealtime(3);

        // Display a "prepare to re-pair" message
        logoImage.SetActive(true);
        logoImage.GetComponent<Animator>().enabled = true;

        // Display a transition effect
        transitionEffect.SetActive(true);
        transitionEffect.GetComponent<Animator>().enabled = true;

        yield return new WaitForSecondsRealtime(0.7f);

        if (levelIndex < levels.Length)
        {
            levelIndex++;
            LoadLevel();
        }
    }

    public IEnumerator LoseLevel()
    {
        // Tkae away the player's control
        SetPlayerControl(false);
        // Wait for 2 seconds
        yield return new WaitForSecondsRealtime(3);

        // Display a "prepare to re-pair" message
        logoImage.SetActive(true);
        logoImage.GetComponent<Animator>().enabled = true;
        // Display a transition effect
        transitionEffect.SetActive(true);
        transitionEffect.GetComponent<Animator>().enabled = true;

        yield return new WaitForSecondsRealtime(0.7f);

        // Load the same level
        LoadLevel();
    }

    private void SetPlayerControl(bool hasControl)
    {
        foreach (GameObject playerObject in players)
        {
            playerObject.GetComponent<PlayerObject>().hasControl = hasControl;
        }
    }

    // Reset all images back to their initial state
    private void ResetCountdownImages()
    {
        // Disable all raw images before the level starts
        foreach (GameObject image in countdownImages)
        {
            //image.GetComponent<Animator>().enabled = false;
            image.SetActive(false);
        }
    }

    private void ResetTransitionImages()
    {
        logoImage.GetComponent<Animator>().enabled = false;
        logoImage.SetActive(false);
        transitionEffect.GetComponent<Animator>().enabled = false;
        transitionEffect.SetActive(false);
    }
}
