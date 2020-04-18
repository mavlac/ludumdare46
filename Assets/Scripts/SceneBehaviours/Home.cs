using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
	public string faderFadeOutTrigger = "FadeOut";
	public float faderDuration = 1f;
	
	
	
	public void GameButtonDown()
	{
		Camera.main.GetComponentInChildren<Animator>().SetTrigger(faderFadeOutTrigger);
		Invoke("LoadNextScene", faderDuration);
	}
	public void QuitButtonDown()
	{
		// TODO
	}



	public void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}