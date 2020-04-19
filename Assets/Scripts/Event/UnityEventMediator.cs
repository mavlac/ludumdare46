using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Holder of UnityEvents, that can be raised via public method
/// useful for animation callbacks - limited ability of Animation Events from animation timeline
/// </summary>
public class UnityEventMediator : MonoBehaviour
{
	[System.Serializable]
	public class MediatorEvent
	{
		[HideInInspector]
		public string rowLabel;
		[Tooltip("Optional description to know why this event is here at all.")]
		public string description;
		public UnityEvent evt;
	}

	public List<MediatorEvent> events;



	private void OnValidate()
	{
		if (events == null || events == default) return;
		
		for (int i = 0; i < events.Count; i++)
			events[i].rowLabel = $"[{i}] {events[i].description}";
	}



	public void RaiseMediatorEvent(int i)
	{
		if (i > events.Count) Debug.LogError(string.Format("Event with index {0} not defined on {1}", i, gameObject.name));
		events[i].evt.Invoke();
	}

}