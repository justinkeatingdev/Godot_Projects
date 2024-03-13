using Godot;
using System;

public partial class SingalManager : Node
{

	public static SingalManager SignalManager;

	[Signal]
	public delegate void OnLevelSelectedEventHandler(int levelNum);

	[Signal]
	public delegate void OnGameExitPressedEventHandler();

	[Signal]
	public delegate void OnSelectionEnabledEventHandler();

	[Signal]
	public delegate void OnSelectionDisabledEventHandler();

	[Signal]
	public delegate void OnTileSelectedEventHandler(MemoryTile tile);

	[Signal]
	public delegate void OnGameOverEventHandler(int moves);


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

	public void EmitOnSelectionEnabled()
	{
		EmitSignal(SignalName.OnSelectionEnabled);
	}

	public void EmitOnSelectionDisabledSelected()
	{
		EmitSignal(SignalName.OnSelectionDisabled);
	}

	public void EmitOnTileSelected(MemoryTile tile)
	{
		EmitSignal(SignalName.OnTileSelected, tile);
	}

	public void EmitOnGameOver(int moves)
	{
		EmitSignal(SignalName.OnGameOver, moves);
	}
}
