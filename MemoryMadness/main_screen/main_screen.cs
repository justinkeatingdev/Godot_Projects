using Godot;
using System;

public partial class main_screen : Control
{

	[Export]
	public PackedScene levelButtonScence;
	public HBoxContainer HBLevels;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HBLevels = GetNode<HBoxContainer>("VB/HBLevels");
		SetupGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CreateLevelButton(int ln)
	{
		var newLB = (level_button)levelButtonScence.Instantiate();
		HBLevels.AddChild(newLB);
		newLB.SetLevelNumber(ln);
	}

	public void SetupGrid()
	{
		foreach (var level in game_manager.levels) 
		{
			CreateLevelButton(game_manager.levels.IndexOf(level));
		}
	}
}
