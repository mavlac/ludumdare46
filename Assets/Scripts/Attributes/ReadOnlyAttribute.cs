using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Makes Property read-only in Inspector
/// 
/// Name of another boolean Property may be defined as optional Attribute parameter,
/// read-only state is then controlled by that bool value.
/// 
/// Target Property may optionally be visualy intended via EditorGUI.indentLevel
/// 
/// Useful for making generic Inspectors with deeper optional setup.
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute
{
	public string linkedBoolPropertyName;
	public bool inverseLinkedBoolValue = false;
	public bool indentPropertyLabel = true;

	public ReadOnlyAttribute()
	{
	}
	public ReadOnlyAttribute(string linkedBoolPropertyName, bool inverseLinkedBoolValue = false, bool indentPropertyLabel = false)
	{
		this.linkedBoolPropertyName = linkedBoolPropertyName;
		this.inverseLinkedBoolValue = inverseLinkedBoolValue;
		this.indentPropertyLabel = indentPropertyLabel;
	}
}



#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var att = (ReadOnlyAttribute)attribute;
		var linkedProperty = property.serializedObject.FindProperty(att.linkedBoolPropertyName);
		
		if (linkedProperty != null)
		{
			// Enable / disable based on linked bool Property value
			bool linkedBoolPropertyValue = linkedProperty.boolValue;
			if (att.inverseLinkedBoolValue) linkedBoolPropertyValue = !linkedBoolPropertyValue;
			GUI.enabled = linkedBoolPropertyValue;

			if (att.indentPropertyLabel)
				EditorGUI.indentLevel++;
		}
		else
		{
			// Basic functionality - just disable this drawn Component
			GUI.enabled = false;
		}
		
		EditorGUI.PropertyField(position, property, label, true);
		if (linkedProperty != null && att.indentPropertyLabel) EditorGUI.indentLevel--;
		
		GUI.enabled = true;
	}
}
#endif