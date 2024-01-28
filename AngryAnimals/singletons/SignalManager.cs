using Godot;
using System;

public partial class SignalManager : Node
{
	[Signal]
	public delegate void OnUpdateDebugEventHandler(string text);

	[Signal]
	public delegate void OnAnimalDiedEventHandler();

	public static SignalManager signalManager;

	public override void _Ready()
	{
		signalManager = this;
	}

	public override void _Process(double delta)
	{
	}

	public void EmitOnUpdateDebugSignal(string s)
	{
		EmitSignal(SignalName.OnUpdateDebug, s);
	}

	public void EmitOnAnimalDiedSignal()
	{
		EmitSignal(SignalName.OnAnimalDied);
	}
}
