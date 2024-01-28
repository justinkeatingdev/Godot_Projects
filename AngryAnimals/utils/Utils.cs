using Godot;
using System;

public partial class Utils
{
	public static string VecToString(Vector2 vec)
	{
		return $"({vec.X.ToString("0.##")}, {vec.Y.ToString("0.##")})";
	}
}
