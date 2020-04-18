using UnityEngine;
using UnityEditor;

/// <summary>
/// Adds empty GameObject as a Hierarchy separator.
/// Sets tag to EditorOnly not to be included in build.
/// Usage: from Editor menu, or directly as a context menu item in Hieararchy
/// </summary>
public class HierarchySeparator
{
	const string separatorGOName = "---";
	const string editorOnlyTag = "EditorOnly";
	
	// Add a menu item to create custom GameObjects
	[MenuItem("GameObject/--- EditorOnly Separator", false, 20)]
	public static void CreateCustomGameObject(MenuCommand menuCommand)
	{
		GameObject go = new GameObject(separatorGOName);
		// Ensure it gets reparented if this was a context click (otherwise does nothing)
		GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
		// Register the creation in the undo system
		Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
		
		go.transform.position = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.tag = editorOnlyTag;
		
		Selection.activeObject = go;
	}
}