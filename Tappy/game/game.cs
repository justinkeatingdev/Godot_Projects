using Godot;
using System;

public partial class game : Node2D
{

	[Export]
	private PackedScene pipesScence;
	//public PackedScene pipesScence = GD.Load<PackedScene>("res://pipes/pipes.tscn");

	private Node pipesHolder;
	private Marker2D spawnU;
	private Marker2D spawnL;
	private Timer spawnTimer;
	private AudioStreamPlayer engineSound;
	private AudioStreamPlayer gameOverSound;
	
	//private GameManager gameManager;

	public override void _Ready()
	{
		GameManager.gameManager.SetScore(0);
		pipesHolder = GetNode<Node>("PipesHolder");
		spawnU = GetNode<Marker2D>("SpawnU");
		spawnL = GetNode<Marker2D>("SpawnL");
		spawnTimer = GetNode<Timer>("SpawnTimer");
		engineSound = GetNode<AudioStreamPlayer>("EngineSound");
		gameOverSound = GetNode<AudioStreamPlayer>("GameOverSound");
		

		//gameManager = (GameManager)GetNode("/root/GameManager");
		GameManager.gameManager.Connect(GameManager.SignalName.OnGameOver, new Callable(this, MethodName.OnGameOver));

		SpawnPipes();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void SpawnPipes()
	{
		Vector2 pipePlacement = new Vector2 (0, 0);
		var yPos = GD.RandRange(spawnU.Position.Y, spawnL.Position.Y);
		var newPipes = (Node2D)pipesScence.Instantiate();

		pipePlacement.X = spawnL.Position.X;
		pipePlacement.Y = (float)yPos;

		newPipes.GlobalPosition = pipePlacement;

		//GetTree().Root.GetNode("PipesHolder").AddChild(newPipes);
		pipesHolder.AddChild(newPipes);
	}

	public void StopPipes()
	{
		spawnTimer.Stop();
		foreach (var pipe in pipesHolder.GetChildren())
		{
			pipe.SetProcess(false);
		}
	}

	public void _on_spawn_timer_timeout()
	{
		SpawnPipes();
	}

	public void OnGameOver()
	{
		StopPipes();
		//var mainScene = (PackedScene)ResourceLoader.Load("res://main/main.tscn");
		//GetTree().ChangeSceneToPacked(mainScene);
		engineSound.Stop();
		gameOverSound.Play();
	}

}
