using Godot;
using System;

public partial class pipes : Node2D
{

	public const float scrollSpeed = 150.0f;
	private Vector2 pipePosition;
	private AudioStreamPlayer scoreSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scoreSound = GetNode<AudioStreamPlayer>("ScoreSound");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		pipePosition = Position;
		pipePosition.X -= scrollSpeed * (float)delta;
		Position = pipePosition;
	}

	public void OnScreenExited()
	{
		QueueFree();
	}

	public void PlayerScored()
	{
		scoreSound.Play();
		GameManager.gameManager.IncrementScore();
	}

	public void _on_pipe_body_entered(Node2D body)
	{
		if (body.IsInGroup(GameManager.GROUP_PLANE))
		{
			place_cb newBody = (place_cb)body;
			newBody.Die();
		}
	}

	public void _on_laser_body_entered(Node2D body)
	{
		if (body.IsInGroup(GameManager.GROUP_PLANE))
		{
			place_cb newBody = (place_cb)body;
			PlayerScored();
		}
	}
}
