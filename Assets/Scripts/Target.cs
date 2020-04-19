using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Target : MonoBehaviour
{
	SpriteRenderer[] allSprites = null;

	public Transform arrows;



	private void OnEnable()
	{
		if (allSprites == null)
		{
			allSprites = GetComponentsInChildren<SpriteRenderer>();
		}
		
		foreach (var s in allSprites)
		{
			s.color = s.color.SetAlpha(0f);
			s.DOFade(1f, 0.25f).SetDelay(1f);
		}

		arrows.localScale = Vector3.zero;
		arrows.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutCubic).SetDelay(1f);
	}

	public void FadeOutAndDisableSelf()
	{
		foreach (var s in allSprites)
		{
			s.DOFade(0f, 0.25f);
		}

		arrows.DOScale(Vector3.one * 1.5f, 0.25f).OnComplete(() =>
		{
			this.gameObject.SetActive(false);
		});
	}
}