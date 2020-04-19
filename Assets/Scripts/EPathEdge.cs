using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class EPathEdge : MonoBehaviour
{
	public EPathNode start;
	public EPathNode end;
	public bool onBackSide;

	[Space]
	public LineRenderer lineRenderer;



	private void OnValidate()
	{
		UpdateLineRendererPositions();
		
		if (onBackSide)
		{
			lineRenderer.enabled = false;
			if (start.endingType == EPathNode.NodeEndingType.None) start.endingType = EPathNode.NodeEndingType.Tunnel;
			if (end.endingType == EPathNode.NodeEndingType.None) end.endingType = EPathNode.NodeEndingType.Tunnel;
			start.UpdateEndingTypeVisual();
			end.UpdateEndingTypeVisual();
		}
		else
		{
			lineRenderer.enabled = true;
			//start.endingType = PathNode.NodeEndingType.None;
			//end.endingType = PathNode.NodeEndingType.None;
			//start.UpdateEndingTypeVisual();
			//end.UpdateEndingTypeVisual();
		}
	}



	public void UpdateLineRendererPositions()
	{
		if (!start || !end) return;
		
		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition(0, start.transform.position);
		lineRenderer.SetPosition(1, end.transform.position);
	}
}