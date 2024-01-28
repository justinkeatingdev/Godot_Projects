using Godot;
using System;

public partial class GameManager : Node
{

	[Signal]
	public delegate void OnGameOverEventHandler();

	[Signal]
	public delegate void OnScoreUpdatedEventHandler();

	public const string GROUP_PLANE = "plane";

	public static GameManager gameManager;

	public PackedScene gameScene = (PackedScene)ResourceLoader.Load("res://game/game.tscn");
	public PackedScene mainScene = (PackedScene) ResourceLoader.Load("res://main/main.tscn");

	public int score = 0;
	public int highScore = 0;

	public int GetScore()
	{
		return score;
	}

	public int GetHighScore()
	{
		return highScore;
	}

	public void SetScore(int v)
	{
		score = v;
		if (score > highScore)
		{
			highScore = score;
		}
		EmitSignal(SignalName.OnScoreUpdated);
		GD.Print($"{score}, {highScore}");
	}

	public void IncrementScore()
	{
		SetScore(score + 1);
	}

	public override void _Ready()
	{
		gameManager = this;
	}

	public void LoadGameScene()
	{
		
		GetTree().ChangeSceneToPacked(gameScene);
	}

	public void LoadMainScene()
	{
		
		GetTree().ChangeSceneToPacked(mainScene);
	}

	public void EmitOnGameOverSignal()
	{
		EmitSignal(SignalName.OnGameOver);
	}
}
