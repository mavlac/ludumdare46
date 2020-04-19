using System.Collections.Generic;

public static class ListExtensions
{
	public static T Random<T>(this T[] array)
	{
		if (array.Length == 0) return default; // default(T) - null, 0..
		return array[UnityEngine.Random.Range(0, array.Length)];
	}
	public static T Random<T>(this List<T> list)
	{
		if (list.Count == 0) return default;
		return list[UnityEngine.Random.Range(0, list.Count)];
	}
	/// <summary>
	/// Shuffle the List using the Fisher-Yates method
	/// </summary>
	public static void Shuffle<T>(this List<T> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}