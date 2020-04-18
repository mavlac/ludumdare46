using UnityEngine;
using UnityEngine.Assertions;

public static class PrefabUtils
{
	/// <summary>
	/// Instantiates an instance of a prefab.
	/// </summary>
	/// <param name="prefabPath">Path to prefab, under Resources/</param>
	/// <returns>The instantiated GameObject</returns>
	public static GameObject InstantiatePrefab(string prefabPath)
	{
		GameObject prefab = Resources.Load(prefabPath) as GameObject;
		Assert.IsNotNull(prefab, string.Format("Failed to load prefab at path: {0}", prefabPath));
		return Object.Instantiate(prefab);
	}
	/// <summary>
	/// Instantiates an instance of a prefab, returning an attached script of the specified type.
	/// </summary>
	/// <typeparam name="T">Type of script to retrieve</typeparam>
	/// <param name="prefabPath">Path to prefab, under Resources/</param>
	/// <returns>The attached script</returns>
	public static T InstantiatePrefab<T>(string prefabPath) where T : Component
	{
		GameObject instance = InstantiatePrefab(prefabPath);
		return instance.GetComponent<T>();
	}
	
	
	/// <summary>
	/// Instantiates an instance of a prefab.
	/// </summary>
	/// <param name="prefab">Prefab GameObject reference</param>
	/// <returns>The instantiated GameObject</returns>
	public static GameObject InstantiatePrefab(GameObject prefab)
	{
		Assert.IsNotNull(prefab, string.Format("Prefab {0} to instantiate not defined", prefab));
		return Object.Instantiate(prefab);
	}
	/// <summary>
	/// Instantiates an instance of a prefab, returning an attached script of the specified type.
	/// </summary>
	/// <typeparam name="T">Type of script to retrieve</typeparam>
	/// <param name="prefab">Prefab GameObject reference</param>
	/// <returns>The attached script</returns>
	public static T InstantiatePrefab<T>(GameObject prefab) where T : Component
	{
		GameObject instance = InstantiatePrefab(prefab);
		return instance.GetComponent<T>();
	}
	/// <summary>
	/// Full feature instantiation of a prefab, returning a value type Tuple of a GameObject and an attached script of the specified type.
	/// </summary>
	/// <typeparam name="T">Type of script to retrieve</typeparam>
	/// <param name="prefab">Prefab GameObject reference</param>
	/// <param name="parent">Parent Transform to parent instance in</param>
	/// <param name="position">World position</param>
	/// <param name="rotation">World rotation</param>
	/// <returns>The instantiated GameObject and the attached script</returns>
	public static (GameObject, T) InstantiatePrefab<T>(
		GameObject prefab,
		Transform parent,
		Vector3 position,
		Quaternion rotation) where T : Component
	{
		Assert.IsNotNull(prefab, string.Format("Prefab {0} to instantiate not defined", prefab));
		GameObject instance = Object.Instantiate(prefab, position, rotation, parent);
		return (instance, instance.GetComponent<T>());
	}
}