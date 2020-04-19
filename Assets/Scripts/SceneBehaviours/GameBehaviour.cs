using MavLib.Variables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
	public string faderFadeOutTrigger = "FadeOut";
	public float faderDuration = 1f;

	[Space]
	public AudioSource introSoundSource;

	[Space]
	public LCD lcd;

	[Header("Timing")]
	public float minBurnTime = 3f;
	public float maxBurnTime = 10f;
	float nextBurn = float.PositiveInfinity;

	[Header("Burned Components")]
	public IntSO burnedComponents;
	public int burnedCompoDamagedThreshold;
	public int burnedCompoDeadThreshold;


	private List<Compo> components;



	private void Awake()
	{
		components = Object.FindObjectsOfType<Compo>().ToList();
		//Debug.Log($"Found {components.Length} Components.");
	}
	private void Start()
	{
		burnedComponents.Value = 0;
		
		Invoke("DelayedIntroSoundPlayback", faderDuration * 0.75f);


		SetNextBurnTime();
	}
	void DelayedIntroSoundPlayback()
	{
		introSoundSource.Play();
	}

	private void OnEnable()
	{
		burnedComponents.OnDidChange += UpdatedBurnedComponentValue;
	}
	private void OnDisable()
	{
		burnedComponents.OnDidChange -= UpdatedBurnedComponentValue;
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.B))
		{
			nextBurn = float.NegativeInfinity;
		}
#endif

		if (Time.time > nextBurn)
		{
			SetNextBurnTime();
			
			var randomCompo = components.Random();
			Debug.Log($"Selecting {randomCompo.gameObject.name} (Burned:{randomCompo.Burned}) to burn");
			if (!randomCompo.Burned)
			{
				randomCompo.Burn();
				Debug.Log($"Burned {randomCompo.gameObject.name} (Burned:{randomCompo.Burned}).");
			}
			else
			{
				Debug.Log($"{randomCompo.gameObject.name} is already burned. Action aborted.");
			}
		}
	}



	void SetNextBurnTime()
	{
		nextBurn = Time.time + Random.Range(minBurnTime, maxBurnTime);
	}



	private void UpdatedBurnedComponentValue()
	{
		if (burnedComponents.Value >= burnedCompoDeadThreshold)
		{
			Debug.Log($"Dead (KO: {burnedComponents.Value})");
			lcd.SetGlitchLevel(2);
		}
		else if (burnedComponents.Value >= burnedCompoDamagedThreshold)
		{
			Debug.Log($"Damaged (KO: {burnedComponents.Value})");
			lcd.SetGlitchLevel(1);
		}
		else
		{
			Debug.Log("Everything ok again");
			lcd.SetGlitchLevel(0);
		}
	}




	public void GetBackToHomeScreen()
	{
		Camera.main.GetComponentInChildren<Animator>().SetTrigger(faderFadeOutTrigger);
		Invoke("LoadHomeScene", faderDuration);
	}



	void LoadHomeScene()
	{
		SceneManager.LoadScene("Home");
	}
}