using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EPath : MonoBehaviour
{
	public List<EPathEdge> edges;

	[Header("Timing")]
	public bool autoTrigger = true;
	public float frequency = 2f;
	public float fRanDelta = 0.5f;
	float nextTriggerTime;

	[Space]
	public GameObject electronPrefab;



	private void Start()
	{
		SetFirstTriggerTime();
	}
	private void Update()
	{
		if (autoTrigger && Time.time > nextTriggerTime)
		{
			TriggerElectronJourney();
			SetNextTriggerTime();
		}
	}



	void SetFirstTriggerTime()
	{
		nextTriggerTime = Time.time + Random.Range(0f, frequency + fRanDelta);
	}
	void SetNextTriggerTime()
	{
		nextTriggerTime = Time.time + frequency + Random.Range(-fRanDelta, fRanDelta);
	}



	public void TriggerElectronJourney()
	{
		EPathNode firstNode = edges[0].start;
		Vector3 iniPos = firstNode.transform.position;
		(GameObject go, Electron el) electron = PrefabUtils.InstantiatePrefab<Electron>(electronPrefab, null, iniPos, Quaternion.identity);
		electron.el.Initialize(edges);
	}



	[AttachAsInspectorButton(runtimeOnly: false)]
	public void FindOwnEdgeChildren()
	{
		edges = GetComponentsInChildren<EPathEdge>().ToList();
		EditorUtility.SetDirty(this);
	}
}