using Godot;
using System;

public partial class scrolling_layer : ParallaxLayer
{
	private Sprite2D Sprite2D;

	[Export]
	private Texture texture;
	[Export]
	private float scrollScale = 0.0f;
	[Export]
	private float tx_x = 1920.0f;
	[Export]
	private float tx_y = 1080.0f;

	private Vector2 motionScale;
	private Vector2 motionMirror;


	public override void _Ready()
	{
		motionScale = MotionScale;
		motionMirror = MotionMirroring;

		Sprite2D = GetNode<Sprite2D>("Sprite2D");

		motionScale.X = scrollScale;

		var scaleF = GetViewportRect().Size.Y / tx_y;

		Sprite2D.Texture = (Texture2D)texture;
		Sprite2D.Scale = new Vector2(scaleF, scaleF);

		motionMirror.X = tx_x * scaleF;

		MotionScale = motionScale;
		MotionMirroring = motionMirror;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

}
