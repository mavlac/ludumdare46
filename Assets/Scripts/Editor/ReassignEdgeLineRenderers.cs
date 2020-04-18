#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class ReassignEdgeLineRenderersHotkey
{
	[MenuItem("Tools/Reassign Edge LineRenderers %w")] // CTRL + W
	private static void ReassignEdgeLineRenderers()
	{
        EPathEdge[] myItems = Object.FindObjectsOfType<EPathEdge>();
        //Debug.Log("Found " + myItems.Length + " instances with this script attached");
        foreach (EPathEdge item in myItems)
        {
            //Debug.Log(item.gameObject.name);
            item.UpdateLineRendererPositions();
        }
    }
}
#endif