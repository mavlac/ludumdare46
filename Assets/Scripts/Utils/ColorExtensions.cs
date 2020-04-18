using UnityEngine;

public static class ColorExtensions
{
	public static Color ClearWhite => Color.white.SetAlpha(0);



	public static Color SetAlpha(this Color color, float alpha)
	{
		color.a = alpha;
		return color;
	}
	/// <summary>
	/// Sets alpha of color to complete transparency (0)
	/// </summary>
	/// <param name="color">Color</param>
	/// <returns>Altered Color</returns>
	public static Color Transparent(this Color color)
	{
		return color.SetAlpha(0f);
	}
	/// <summary>
	/// Sets alpha of color to half transparency (0.5)
	/// </summary>
	/// <param name="color">Color</param>
	/// <returns>Altered Color</returns>
	public static Color Translucent(this Color color)
	{
		return color.SetAlpha(0.5f);
	}
	/// <summary>
	/// Sets alpha of color to opaque (1)
	/// </summary>
	/// <param name="color">Color</param>
	/// <returns>Altered Color</returns>
	public static Color Opaque(this Color color)
	{
		return color.SetAlpha(1f);
	}
	/// <summary>
	/// Sets alpha of color to one half of current alpha (alpha * 0.5)
	/// </summary>
	/// <param name="color">Color</param>
	/// <returns>Altered Color</returns>
	public static Color HalveAlpha(this Color color)
	{
		return color.SetAlpha(color.a * 0.5f);
	}
}