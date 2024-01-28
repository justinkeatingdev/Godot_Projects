using Godot;
using System;

public partial class hud : Control
{
	private Label ScoreLabel;
	public override void _Ready()
	{
		ScoreLabel = GetNode<Label>("MC/ScoreLabel");

		GameManager.gameManager.Connect(GameManager.SignalName.OnScoreUpdated, new Callable(this, MethodName.OnScoreUpdated));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnScoreUpdated()
	{
		ScoreLabel.Text = GameManager.gameManager.GetScore().ToString();
	}
}
