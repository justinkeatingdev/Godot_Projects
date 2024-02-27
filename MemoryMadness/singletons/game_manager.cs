using Godot;
using System;
using System.Collections.Generic;

public partial class game_manager : Node
{
	// Called when the node enters the scene tree for the first time.

	public static game_manager game_Manager;

	public static List<level> levels = new List<level> 
	{
		{new level(2, 2)},
		{new level(3, 4)},
		{new level(4, 4)},
		{new level(4, 6)},
		{new level(5, 6)},
		{new level(6, 6)}
	};

	public override void _Ready()
	{
		game_Manager = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

public class level
{
	public int Rows;
	public int Cols;

	public level(int rows, int cols)
	{
		Rows = rows;
		Cols = cols;
	}
}
