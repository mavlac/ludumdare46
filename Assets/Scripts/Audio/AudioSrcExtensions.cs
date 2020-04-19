using System.Collections;
using UnityEngine;

public static class AudioSrcExtensions
{
	/// <summary>
	/// Fades out the AudioSource over specified time and stops when fade finished
	/// </summary>
	/// <param name="audioSrc">Target of extension method</param>
	/// <param name="fadeOutLength">Duration of fade</param>
	/// <param name="monoBehaviour">MonoBehaviour to start the Coroutine on</param>
	public static void StopWithFadeOut(this AudioSource audioSrc, float fadeOutLength, MonoBehaviour monoBehaviour)
	{
		monoBehaviour.StartCoroutine(FadeOutCoroutine(audioSrc, fadeOutLength));
	}
	static IEnumerator FadeOutCoroutine(AudioSource target, float fadeOutLength)
	{
		float initialVolume = target.volume;
		
		for (float t = 0; t <= fadeOutLength; t += Time.deltaTime)
		{
			target.volume = Mathf.SmoothStep(initialVolume, 0f, Mathf.InverseLerp(0f, fadeOutLength, t));
			yield return null;
		}
		
		target.Stop();
		target.volume = initialVolume;
	}
}