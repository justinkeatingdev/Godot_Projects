using Godot;
using System;
using System.Reflection.Metadata;

public partial class front_sprite : TextureRect
{

	public Vector2 SCALE_SMALL = new Vector2(0.1f, 0.1f);
	public Vector2 SCALE_NORMAL = new Vector2(1.0f, 1.0f);
	public Vector2 SPIN_TIME_RANGE = new Vector2(1.0f, 2.0f);
	public float SCALE_TIME = 0.5f;

	public override void _Ready()
	{
		RunMe();
	}

	public float GetRandomSpinTime()
	{
		return (float)GD.RandRange(SPIN_TIME_RANGE.X, SPIN_TIME_RANGE.Y);
	}

	public void RunMe()
	{
		var tween = GetTree().CreateTween();
		tween.SetLoops();
		tween.TweenProperty(this, "scale", SCALE_NORMAL, SCALE_TIME);
		tween.TweenProperty(this, "scale", SCALE_SMALL, SCALE_TIME);
		
	}
}
