using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Clickable button using SpriteRenderer and some trigger Collider2D
/// Aim is to provide something similar to Canvas Button without using Canvas, RectTransforms, Image etc.
/// </summary>
public class SpriteButton : MonoBehaviour
{
	public enum ButtonState
	{
		Ready,
		Hover,
		Active
	}

	[SerializeField]
	private bool interactable = true;
	public bool Interactable
	{
		get { return interactable; }
		set
		{
			// Interactable value changed
			if (value != interactable)
			{
				interactable = value;
			}
		}
	}

	public bool disableAfterClicked = false;

	public SpriteRenderer targetGraphic;

	[Space]
	public Color hoverTint = Color.white;
	public Sprite hoverSprite;
	public Color activeTint = Color.white;
	public Sprite activeSprite;
	public Color disabledTint = Color.gray;
	public Sprite disabledSprite;
	
	[Space]
	public float tintFadeDuration = 0.15f;
	
	[Space]
	public UnityEvent onClick;


	ButtonState state = ButtonState.Ready;

	Coroutine tintCoroutine;

	Sprite initialSprite;
	Color initialColor;



	private void Reset()
	{
		// Try to find SpriteRenderer on current GameObject
		targetGraphic = GetComponent<SpriteRenderer>();
		if (targetGraphic == null) GetComponentInChildren<SpriteRenderer>();

		// If there is no Collider2D, create one
		// In both cases set to Trigger
		Collider2D col = GetComponent<Collider2D>();
		if (col == null)
		{
			col = gameObject.AddComponent<BoxCollider2D>();
		}
		col.isTrigger = true;

		ResetStateSprites();
	}
	private void OnValidate()
	{
		// Inspector triggering of interactability
		// Runtime only, because we have the default Sprite and Color saved as initials from Awake
		if (initialSprite == default || initialColor == default) return;
		
		SetVisualState(ButtonState.Ready, interactable);
	}
	private void Awake()
	{
		initialColor = targetGraphic.color;
		initialSprite = targetGraphic.sprite;
		
		
		SetVisualState(state, interactable);
	}
	private void OnMouseEnter()
	{
		SetVisualState(state = ButtonState.Hover, interactable);
	}
	private void OnMouseExit()
	{
		SetVisualState(state = ButtonState.Ready, interactable);
	}
	private void OnMouseDown()
	{
		SetVisualState(state = ButtonState.Active, interactable);
	}
	private void OnMouseUpAsButton()
	{
		if (!Interactable) return;
		
		if (disableAfterClicked) Interactable = false;
		SetVisualState(state = ButtonState.Hover, interactable);
		
		onClick.Invoke();
		//Debug.Log($"Button {gameObject.name} clicked.");
	}



	[ContextMenu("Assign state Sprites from Target Graphic")]
	private void ResetStateSprites()
	{
		hoverSprite = activeSprite = disabledSprite = targetGraphic.sprite;
	}




	public void SetVisualState(ButtonState state, bool interactable)
	{
		// Disabled state
		if (!interactable)
		{
			targetGraphic.sprite = disabledSprite;
			TintGraphic(disabledTint);
		}
		else
		{
			switch(state)
			{
				case ButtonState.Ready:
					targetGraphic.sprite = initialSprite;
					TintGraphic(initialColor);
					break;
				case ButtonState.Hover:
					targetGraphic.sprite = hoverSprite;
					TintGraphic(hoverTint);
					break;
				case ButtonState.Active:
					targetGraphic.sprite = activeSprite;
					TintGraphic(activeTint);
					break;
			}
		}
	}
	
	void TintGraphic(Color targetColor)
	{
		if (tintCoroutine != null) StopCoroutine(tintCoroutine);
		tintCoroutine = StartCoroutine(TintCoroutine(targetColor));
	}
	IEnumerator TintCoroutine(Color targetColor)
	{
		Color fadeInitColor = targetGraphic.color;
		
		if (tintFadeDuration > 0f)
		{
			for (float t = 0f; t <= tintFadeDuration; t += Time.deltaTime)
			{
				targetGraphic.color =
					Color.Lerp(
						fadeInitColor,
						targetColor,
						Mathf.SmoothStep(0f, 1f, t / tintFadeDuration));

				yield return null;
			}
		}
		
		targetGraphic.color = targetColor;
	}
}