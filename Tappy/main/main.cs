using Godot;
using System;

public partial class main : Control
{
	private Label HighScoreLabel;

	public override void _Ready()
	{
		HighScoreLabel = GetNode<Label>("MC/HB/HighScoreLabel");

		HighScoreLabel.Text = GameManager.gameManager.GetHighScore().ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("fly"))
		{
			GameManager gameManager = (GameManager)GetNode("/root/GameManager");
			gameManager.LoadGameScene();
		}
	}
}
