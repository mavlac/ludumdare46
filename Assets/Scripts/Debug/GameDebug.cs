using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebug : MonoBehaviour
{
#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R)) RestartCurrentScene();
		
		if (Input.GetKeyDown(KeyCode.F))
		{
			// insert whatever debug actions needed
		}
	}
#endif
	
	public void RestartCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}