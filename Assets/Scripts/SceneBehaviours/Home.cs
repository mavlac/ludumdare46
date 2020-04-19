using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
	public string faderFadeOutTrigger = "FadeOut";
	public float faderDuration = 1f;

	[Space]
	public AudioSource musicAudioSource;
	public AudioSource commonAudio;
	public AudioClip buttonClickClip;



	private void Start()
	{
		Invoke("DelayedMusicPlayback", faderDuration * 0.75f);
	}
	void DelayedMusicPlayback()
	{
		musicAudioSource.Play();
	}



	public void GameButtonDown()
	{
		//musicAudioSource.StopWithFadeOut(faderDuration * 0.75f, this);
		musicAudioSource.Stop();
		commonAudio.PlayOneShot(buttonClickClip);
		
		Camera.main.GetComponentInChildren<Animator>().SetTrigger(faderFadeOutTrigger);
		Invoke("LoadNextScene", faderDuration);
	}
	public void QuitButtonDown()
	{
#if !UNITY_WEBGL
		//musicAudioSource.StopWithFadeOut(faderDuration * 0.75f, this);
		musicAudioSource.Stop();
		commonAudio.PlayOneShot(buttonClickClip);
		
		Camera.main.GetComponentInChildren<Animator>().SetTrigger(faderFadeOutTrigger);
		Invoke("Exit", faderDuration);
#else
		Debug.Log("Do nothing on WebGL");
#endif
	}



	void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	void Exit()
	{
#if UNITY_EDITOR
		Debug.Log("Exit() called in Editor. Player stopped.");
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			AndroidJavaObject unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
			unityActivity.Call<bool>("moveTaskToBack", true);
		}
#elif UNITY_WEBGL
		return; // Do nothing
#else
		Application.Quit();
#endif
	}
}