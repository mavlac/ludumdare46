﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Electron : MonoBehaviour
{
	public float movementSpeed = 1f;

	[Space]
	public SpriteRenderer spriteRenderer;
	public SpriteRenderer glowRenderer;
	Color defaultSpriteColor;
	Color defaultGlowColor;


	private List<EPathEdge> edgesToFollow;
	private int edgeIndex;


	bool travel = false;
	Vector3 destinationPosition;


	private void Awake()
	{
		defaultSpriteColor = spriteRenderer.color;
		defaultGlowColor = glowRenderer.color;
	}


	public void Initialize(List<EPathEdge> edgesToFollow)
	{
		this.edgesToFollow = edgesToFollow;
		edgeIndex = 0;
		SetNextDestination(edgesToFollow[edgeIndex]);

		// Fade In
		glowRenderer.color = defaultGlowColor.SetAlpha(0f);
		glowRenderer.DOFade(defaultGlowColor.a, 0.15f);
		spriteRenderer.color = defaultSpriteColor.SetAlpha(0f);
		spriteRenderer.DOFade(defaultSpriteColor.a, 0.15f);
		travel = true;
		/*spriteRenderer.DOFade(defaultColor.a, 0.15f).OnComplete(() =>
		{
			travel = true;
		});*/
	}



	private void Update()
	{
		if (!travel) return;
		
		
		float step = movementSpeed * Time.deltaTime; // Calculate distance to move
		transform.position = Vector3.MoveTowards(transform.position, destinationPosition, step);
		
		// Check if the position of the Electron and Node are approximately equal
		if (Vector3.Distance(transform.position, destinationPosition) < 0.001f)
		{
			DestinationReached();
		}
	}



	void DestinationReached()
	{
		if (edgeIndex == edgesToFollow.Count - 1)
		{
			// Journey finished
			travel = false;
			glowRenderer.DOFade(0f, 0.25f);
			spriteRenderer.DOFade(0f, 0.25f).OnComplete(() =>
			{
				Destroy(this.gameObject);
			});
		}
		else
		{
			edgeIndex++;
			SetNextDestination(edgesToFollow[edgeIndex]);
		}
	}

	private void SetNextDestination(EPathEdge edge)
	{
		destinationPosition = edge.end.transform.position;
	}
}