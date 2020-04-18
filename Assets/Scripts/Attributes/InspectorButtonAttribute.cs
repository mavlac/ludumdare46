//
// Atribute to expose MonoBehaviour's Method call as a Button in Inspector.
// Design and runtime, or runtime only.
//
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Method)]
public class AttachAsInspectorButtonAttribute : PropertyAttribute
{
	public bool runtimeOnly;
	
	/// <summary>
	/// Attach Method as a Button to the bottom of MonoBehaviour's Inspector
	/// </summary>
	/// <param name="runtimeOnly">Disable button when Player is not running.</param>
	public AttachAsInspectorButtonAttribute(bool runtimeOnly = true)
	{
		this.runtimeOnly = runtimeOnly;
	}
}