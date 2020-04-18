//
// Extending default Transform Inspector, now with Reset functionality
//
// Credit for hooking up to default Inspector goes to Cobo3 from Unity Forums
// https://forum.unity.com/threads/extending-instead-of-replacing-built-in-inspectors.407612/
//

using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform), true), CanEditMultipleObjects]
public class TransformCustomInspectorDrawer : Editor
{
	Editor defaultEditor; // Unity's built-in editor
	Transform transform;

	void OnEnable()
	{
		// When this inspector is created, also create the built-in inspector
		defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));
		transform = target as Transform;
	}

	void OnDisable()
	{
		// When OnDisable is called, the default editor we created should be destroyed to avoid memory leakage.
		// Also, make sure to call any required methods like OnDisable
		MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		if (disableMethod != null)
			disableMethod.Invoke(defaultEditor, null);
		
		DestroyImmediate(defaultEditor);
	}

	public override void OnInspectorGUI()
	{
		defaultEditor.OnInspectorGUI();


		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUIUtility.labelWidth);
		if (transform.localPosition != Vector3.zero)
		{
			if (GUILayout.Button("Reset Position"))
			{
				Undo.RegisterCompleteObjectUndo(target, $"Reset {transform.gameObject.name} Transform");
				transform.localPosition = Vector3.zero;
			}
		}
		if (transform.localRotation != Quaternion.identity)
		{
			if (GUILayout.Button("Reset Rotation"))
			{
				Undo.RegisterCompleteObjectUndo(target, $"Reset {transform.gameObject.name} Transform");
				transform.localRotation = Quaternion.identity;
			}
		}
		if (transform.localScale != Vector3.one)
		{
			if (GUILayout.Button("Reset Scale"))
			{
				Undo.RegisterCompleteObjectUndo(target, $"Reset {transform.gameObject.name} Transform");
				transform.localScale = Vector3.one;
			}
		}
		GUILayout.EndHorizontal();
	}
}