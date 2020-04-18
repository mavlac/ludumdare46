using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ExecuteAlways]
public class BlurAmount : MonoBehaviour
{
	public Material blurMaterial;
	public float blurAmount;

	private void Update()
	{
		blurMaterial.SetFloat("_Size", blurAmount);
	}
}