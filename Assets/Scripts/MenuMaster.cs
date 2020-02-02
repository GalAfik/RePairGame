using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
	// Go to the next scene
	public void Play()
	{
		SceneManager.LoadScene("Main");
	}

	// Exit the game and close the application
	public void Exit()
	{
		Application.Quit();
	}
}
