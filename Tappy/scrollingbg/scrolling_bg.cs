using Godot;
using System;

public partial class scrolling_bg : ParallaxBackground
{
	public const float SPEED = 120.0f;

	private Vector2 scrollOffset;
	public override void _Ready()
	{
		GameManager.gameManager.Connect(GameManager.SignalName.OnGameOver, new Callable(this, MethodName.OnGameOver));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		scrollOffset = ScrollOffset;
		scrollOffset.X += SPEED * (float)delta * -1;
		ScrollOffset = scrollOffset;
	}

	public void OnGameOver()
	{
		SetProcess(false);
	}
}
