﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
	public string faderFadeOutTrigger = "FadeOut";
	public float faderDuration = 1f;

	[Space]
	public AudioSource introSoundSource;



	private void Start()
	{
		Invoke("DelayedIntroSoundPlayback", faderDuration * 0.75f);
	}
	void DelayedIntroSoundPlayback()
	{
		introSoundSource.Play();
	}




	public void GetBackToHomeScreen()
	{
		Camera.main.GetComponentInChildren<Animator>().SetTrigger(faderFadeOutTrigger);
		Invoke("LoadHomeScene", faderDuration);
	}



	void LoadHomeScene()
	{
		SceneManager.LoadScene("Home");
	}
}