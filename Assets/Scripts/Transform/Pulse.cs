using UnityEngine;

/// <summary>
/// Loop-updates LocalScale periodically by multiplying it by progression AnimationCurve value in time
/// </summary>
public class Pulse : MonoBehaviour
{
	public float speed = 1f;
	public AnimationCurve localScaleProgression = AnimationCurveUtils.PulseRelative01;
	
	
	Vector3 initialLocalScale;
	
	float t = 0f;
	
	
	
	private void Awake()
	{
		initialLocalScale = transform.localScale;
	}
	private void Update()
	{
		t += Time.smoothDeltaTime * speed;
		if (t > 1f) t -= 1f;
		
		transform.localScale = initialLocalScale * localScaleProgression.Evaluate(t);
	}
}