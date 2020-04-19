using MavLib.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class LCD : MonoBehaviour
{
	public Animator screenAnimator;
	public Animator glitchAnimator;

	public UnityEvent onIntroFinished;
	public UnityEvent onHappy;

	public AudioSource commonAudio;
	public AudioClip actionDeniedSound;

	[Space]
	public IntSO burnedComponents;


	int glitchLevel = 0;


	public void SetGlitchLevel(int glitchLevel)
	{
		if (this.glitchLevel == glitchLevel) return;
		
		this.glitchLevel = glitchLevel;
		
		glitchAnimator.SetInteger("GlitchLevel", glitchLevel);
	}



	public void TriggerState(string lcdAnimatorTrigger)
	{
		// No state triggering if circuit not complete
		if (burnedComponents.Value > 0)
		{
			commonAudio.Stop();
			commonAudio.clip = actionDeniedSound;
			commonAudio.Play();
			return;
		}
		
		screenAnimator.SetTrigger(lcdAnimatorTrigger);
	}


	public void IntroFinishedAnimationCallback()
	{
		onIntroFinished.Invoke();
	}
	public void HappyAnimationCallback()
	{
		onHappy.Invoke();
	}
}