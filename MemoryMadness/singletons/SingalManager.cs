using Godot;
using System;

public partial class SingalManager : Node
{

	public static SingalManager SignalManager;

	[Signal]
	public delegate void OnLevelSelectedEventHandler(int levelNum);

	[Signal]
	public delegate void OnGameExitPressedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalManager = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void EmitOnGameExitPressed()
	{
		EmitSignal(SignalName.OnGameExitPressed);
	}

	public void EmitOnLevelSelected(int levelNumber)
	{
		EmitSignal(SignalName.OnLevelSelected, levelNumber);
	}
}
