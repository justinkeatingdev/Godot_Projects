using Godot;
using System;

public partial class GameOver : Control
{
	public Label MovesLabel;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MovesLabel = GetNode<Label>("NinePatchRect/MC/VB/HB/MovesNumber");

		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnGameOver, new Callable(this, MethodName.OnGameOver));

	}

	public void _on_exit_button_pressed()
	{
		Hide();
		SingalManager.SignalManager.EmitOnGameExitPressed();
	}

	public void OnGameOver(int moves)
	{		
		MovesLabel.Text = moves.ToString();
		Show();
	}
}
