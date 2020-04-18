//
// Some credit and wisdom coming from here:
// https://www.reddit.com/r/Unity3D/comments/1cokm3/extending_the_unity_editor_a_guide_to_creating/
//
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
public class AttachAsInspectorButtonDrawer : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		
		
		var mono = target as MonoBehaviour;
		var methods = mono.GetType()
			.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			.Where(o => Attribute.IsDefined(o, typeof(AttachAsInspectorButtonAttribute)));
		
		if (methods.Count() > 0)
		{
			EditorGUILayout.Space();
			EditorGUIExtras.DrawGUILayoutLine();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Dirty Method Exposures", GUILayout.Width(EditorGUIUtility.labelWidth - 5));
			EditorGUILayout.BeginVertical();

			foreach (var memberInfo in methods)
			{
				var method = memberInfo as MethodInfo;
				bool enabledRuntimeOnly = method.GetCustomAttribute<AttachAsInspectorButtonAttribute>().runtimeOnly;
				
				EditorGUI.BeginDisabledGroup(!Application.isPlaying && enabledRuntimeOnly);
				if (GUILayout.Button(memberInfo.Name + "()"))
				{
					method.Invoke(mono, null);
				}
				EditorGUI.EndDisabledGroup();
			}
			
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndHorizontal();
		}
	}
}