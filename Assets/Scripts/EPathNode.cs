using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EPathNode : MonoBehaviour
{
	public enum NodeEndingType
	{
		None,
		Tunnel,
		Hole,
		HoleSquare,
		Pad
	}

	public NodeEndingType endingType = NodeEndingType.None;

	[Space]
	public SpriteRenderer spriteRenderer;
	public Sprite tunnelSprite;
	public Sprite holeSprite;
	public Sprite holeSquareSprite;
	public Sprite padSprite;



	private void OnValidate()
	{
		UpdateEndingTypeVisual();
	}



	public void UpdateEndingTypeVisual()
	{
		switch (endingType)
		{
			case NodeEndingType.None: spriteRenderer.sprite = null; break;
			case NodeEndingType.Tunnel: spriteRenderer.sprite = tunnelSprite; break;
			case NodeEndingType.Hole: spriteRenderer.sprite = holeSprite; break;
			case NodeEndingType.HoleSquare: spriteRenderer.sprite = holeSquareSprite; break;
			case NodeEndingType.Pad: spriteRenderer.sprite = padSprite; break;
		}
#if UNITY_EDITOR
		EditorUtility.SetDirty(this);
#endif
	}
}