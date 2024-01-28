using Godot;
using System;

public partial class level : Node2D
{

	public PackedScene animalScence = GD.Load<PackedScene>("res://animal/animal.tscn");
	public static Label DebugLabel;
	public static Marker2D AnimalStart;

	public override void _Ready()
	{
		DebugLabel = GetNode<Label>("DebugLabel");
		AnimalStart = GetNode<Marker2D>("AnimalStart");

		SignalManager.signalManager.Connect(SignalManager.SignalName.OnUpdateDebug, new Callable(this, MethodName.OnUpdateDebug));
		SignalManager.signalManager.Connect(SignalManager.SignalName.OnAnimalDied, new Callable(this, MethodName.OnAnimalDied));
		OnAnimalDied();
	}

	public override void _Process(double delta)
	{
	}

	public void OnUpdateDebug(string text)
	{
		DebugLabel.Text = text;
	}

	public void OnAnimalDied()
	{
		RigidBody2D animal = (RigidBody2D)animalScence.Instantiate();
		animal.GlobalPosition = AnimalStart.GlobalPosition;
		AddChild(animal);
	}
}
