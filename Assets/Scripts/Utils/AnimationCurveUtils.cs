using UnityEngine;

public static class AnimationCurveUtils
{
	public static AnimationCurve EaseOut(float startTime, float startValue, float endTime, float endValue)
	{
		return new AnimationCurve(new Keyframe(startTime, startValue, 0, 1), new Keyframe(endTime, endValue, 0, 1));
	}
	public static AnimationCurve EaseOut01 => EaseOut(0, 0, 1, 1);
	public static AnimationCurve LongEaseOut01 =>
		new AnimationCurve(new Keyframe(0, 0, 2, 2), new Keyframe(0.5f, 0.88f, 0.737f, 0.737f), new Keyframe(1, 1, 0, 0));
	
	
	public static AnimationCurve EaseIn(float startTime, float startValue, float endTime, float endValue)
	{
		return new AnimationCurve(new Keyframe(startTime, startValue, 1, 0), new Keyframe(endTime, endValue, 1, 0));
	}
	public static AnimationCurve EaseIn01 => EaseIn(0, 0, 1, 1);
	public static AnimationCurve LongEaseIn01 =>
		new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.5f, 0.12f, 0.835f, 0.835f), new Keyframe(1, 1, 2, 2));
	
	
	public static AnimationCurve EaseInOut(float startTime, float startValue, float endTime, float endValue)
	{
		return new AnimationCurve(new Keyframe(startTime, startValue, 0, 0), new Keyframe(endTime, endValue, 0, 0));
	}
	public static AnimationCurve EaseInOut01 => AnimationCurve.EaseInOut(0, 0, 1, 1);



	/// <summary>
	/// Value pulse from 1 to 1.1 and back to 1 in time range defined by [0..1]
	/// </summary>
	public static AnimationCurve Pulse01 =>
		new AnimationCurve(
			new Keyframe(0, 1, 0, 0),
			new Keyframe(0.5f, 1.1f, 0, 0),
			new Keyframe(1, 1, 0, 0));
	
	/// <summary>
	/// Value pulse from 0.95 to 1.05 and back to 0.95 in time range defined by [0..1]
	/// </summary>
	public static AnimationCurve PulseRelative01 =>
		new AnimationCurve(
			new Keyframe(0, 0.95f, 0, 0),
			new Keyframe(0.5f, 1.05f, 0, 0),
			new Keyframe(1, 0.95f, 0, 0));
	
	public static AnimationCurve RiseAndFall01 =>
		new AnimationCurve(
			new Keyframe(0, 0, 0, 0),
			new Keyframe(0.4f, 0.95f, 1.08f, 1.15f),
			new Keyframe(0.5f, 1, 0, 0),
			new Keyframe(0.6f, 0.95f, -1.30f, -1.37f),
			new Keyframe(1, 0, 0, 0));
	
	public static AnimationCurve BounceOut01 =>
		new AnimationCurve(
			new Keyframe(0, 0, 0, 0),
			new Keyframe(0.25f, 0.05f, 0, 0),
			new Keyframe(1, -1, -1.4f, 0));



	/// <summary>
	/// Helper extension method for reading individual Keyframe parameters
	/// </summary>
	/// <param name="inspectedCurve">AnimationCurve to inspect</param>
	/// <returns>Human readable formatted string</returns>
	public static string DumpAnimationCurveKeys(this AnimationCurve inspectedCurve)
	{
		string dump = default;
		dump += "@time value inTangent/outTangent\n";
		
		foreach (Keyframe k in inspectedCurve.keys)
		{
			dump += string.Format("@{0} {1} in/out {2}/{3}\n", k.time, k.value, k.inTangent, k.outTangent);
		}
		
		return dump;
	}
}