using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject Timer; // The timer object used for keeping time in game

    // Start is called before the first frame update
    void Start()
    {
        // Freeze time as the game starts
        Time.timeScale = 0;
    }

    private void Update()
    {
        // When the player presses the Submit button (Enter), start the level
        if (Input.GetButtonDown("Submit"))
        {
            StartCoroutine(StartLevel());
        }
    }

    // Play as the level starts to begin the countdown and give players control over their player objects
    IEnumerator StartLevel()
    {
        yield return new WaitForSecondsRealtime(3);

    }
}
