using DG.Tweening;
using MavLib.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Compo : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip burnAudio;
	public AudioClip restoreAudio;
	public AudioClip restoredAllAudio;

	[Space]
	public Target targetChildPrefab;

	[Space]
	public GameObject burnFxPrefab;

	[Space]
	public IntSO burnedComponents;


	public bool Burned { get; private set; } = false;

	SpriteRenderer spriteRenderer;
	Collider2D col;



	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
	}
	private void OnMouseDown()
	{
#if UNITY_EDITOR
		if (!Burned) Burn(); else Restore();
#else
		if (Burned) Restore();
#endif
	}



	public void Burn()
	{
		spriteRenderer.DOFade(0.1f, 0.5f);
		
		// Spawn and destroy burning effect
		Destroy(
			Instantiate(burnFxPrefab, this.transform.position, Quaternion.identity) as GameObject,
			5f);

		// Enable targeting prefab
		targetChildPrefab.gameObject.SetActive(true);

		Burned = true;
		burnedComponents.Value = burnedComponents + 1;
		
		audioSource.PlayOneShot(burnAudio);
	}
	public void Restore()
	{
		transform.localScale = Vector3.one * 2f;
		transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutCubic);
		spriteRenderer.DOFade(1f, 0.5f).SetEase(Ease.OutCubic).OnComplete(() =>
		{
			Burned = false;
		});

		targetChildPrefab.FadeOutAndDisableSelf();

		burnedComponents.Value = burnedComponents - 1;

		if (burnedComponents.Value == 0)
		{
			audioSource.PlayOneShot(restoredAllAudio);
		}
		else
		{
			audioSource.PlayOneShot(restoreAudio);
		}
	}
}