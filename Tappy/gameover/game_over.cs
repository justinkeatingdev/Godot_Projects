using Godot;
using System;

public partial class game_over : Control
{
	private Label GameOverLabel;
	private Label PressSpace;
	private Timer Timer;
	//private GameManager gameManager;

	private bool canPressSpace = false;

	public override void _Ready()
	{
		GameOverLabel = GetNode<Label>("GameOverLabel");
		PressSpace = GetNode<Label>("PressSpace");
		Timer = GetNode<Timer>("Timer");

		//gameManager = (GameManager)GetNode("/root/GameManager");
		GameManager.gameManager.Connect(GameManager.SignalName.OnGameOver, new Callable(this, MethodName.OnGameOver));

		//OnGameOver();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (canPressSpace)
		{
			if (Input.IsActionJustPressed("fly"))
			{
				//GameManager gameManager = (GameManager)GetNode("/root/GameManager");
				GameManager.gameManager.LoadMainScene();
			}
		}
	}

	public void OnGameOver()
	{
		Show();
		Timer.Start();
	}

	public void RunSequence()
	{
		GameOverLabel.Hide();
		PressSpace.Show();
		canPressSpace = true;
	}

	public void _on_timer_timeout()
	{
		RunSequence();
	}
}
