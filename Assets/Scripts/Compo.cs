using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Compo : MonoBehaviour
{
	bool burned = false;

	[Space]
	public GameObject burnFxPrefab;

	SpriteRenderer spriteRenderer;
	Collider2D col;



	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
	}
	private void OnMouseDown()
	{
		if (!burned) Burn(); else Restore();
	}



	public void Burn()
	{
		spriteRenderer.DOFade(0.1f, 0.5f);
		
		// Spawn and destroy burning effect
		Destroy(
			Instantiate(burnFxPrefab, this.transform.position, Quaternion.identity) as GameObject,
			5f);
		
		burned = true;
	}
	public void Restore()
	{
		spriteRenderer.DOFade(1f, 0.5f);
		
		burned = false;
	}
}