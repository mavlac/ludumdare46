using UnityEngine;
using UnityEngine.UI;

namespace MavLib.Variables
{
	public class ExampleChangeListener : MonoBehaviour
	{
		public IntSO score;
		
		
		
		private void OnEnable()
		{
			score.OnDidChange += UpdateScoreValue;
		}
		private void OnDisable()
		{
			score.OnDidChange -= UpdateScoreValue;
		}
		
		
		
		private void UpdateScoreValue()
		{
			GetComponent<Text>().text = score.Value.ToString();
		}
	}
}