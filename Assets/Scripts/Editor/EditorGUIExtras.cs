using UnityEngine;
using UnityEditor;

/// <summary>
/// Some EditorGUI and EditorGUILayout goodies for custom Inspectors and owner-drawn Editor expansions
/// </summary>
public static class EditorGUIExtras
{
	public static Color EditorBackgroundColor => EditorGUIUtility.isProSkin ? editorBgColorPro : editorBgColor;
	static readonly Color editorBgColor = new Color(0.76f, 0.76f, 0.76f);
	static readonly Color editorBgColorPro = new Color(0.22f, 0.22f, 0.22f);

	public static Color LineColor => EditorGUIUtility.isProSkin ? lineColorPro : lineColor;
	static readonly Color lineColor = new Color(0.45f, 0.45f, 0.45f);
	static readonly Color lineColorPro = new Color(0.35f, 0.35f, 0.35f);
	
	
	
	public static void DrawGUILayoutLine()
	{
		DrawGUILayoutLine(LineColor);
	}
	public static void DrawGUILayoutLine(Color color, int thickness = 1, int padding = 10)
	{
		Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
		r.height = thickness;
		r.y += padding / 2;
		r.x -= 2;
		r.width += 6;
		EditorGUI.DrawRect(r, color);
	}
}