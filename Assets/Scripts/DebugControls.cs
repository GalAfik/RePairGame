using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugControls : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Quit the game on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
