//
// Changes SpriteRenderer alpha through time
//
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFade : MonoBehaviour
{
	public bool playOnAwake;
	public bool disableRendererOnAwake;
	public AnimationCurve fadeProgression = AnimationCurve.EaseInOut(0,0,1,1);
	public float transitionSpeed = 2f;

	[Space]
	public UnityEvent onFadeFinished;

	bool running = false;
	float t = 0f;

	SpriteRenderer sr;


	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();

		if (playOnAwake) TriggerAnimation();
		else if (disableRendererOnAwake) sr.enabled = false;
	}
	void Update()
	{
		if (!running) return;

		t = Mathf.Clamp01(t + Time.deltaTime * transitionSpeed);

		SetSpriteAlpha(fadeProgression.Evaluate(t));

		if (t == 1f)
		{
			onFadeFinished.Invoke();
			//this.enabled = false;
			running = false;
			return;
		}
	}



	public void TriggerAnimation()
	{
		SetSpriteAlpha(fadeProgression.Evaluate(0f));
		sr.enabled = true;

		t = 0f;
		running = true;
	}



	void SetSpriteAlpha(float a)
	{
		if (!sr) return;

		Color c = sr.color;
		c.a = a;
		sr.color = c;

		if (a == 0)
			sr.enabled = false;
		else if (!sr.enabled) sr.enabled = true;
	}

}