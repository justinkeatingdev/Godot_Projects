using Godot;
using System;
using System.Linq;
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

	public void SetRandomImage()
	{
		var fullPath = image_manager.image_Manager.GetRandomItemImage().LastOrDefault().Value;
		var texture = GD.Load<Texture2D>(fullPath);
		this.Texture = texture;
	}

	public float GetRandomSpinTime()
	{
		float randomSpinTime = (float)GD.RandRange(SPIN_TIME_RANGE.X, SPIN_TIME_RANGE.Y);
		return randomSpinTime;
	}

	public float GetRandomRotation()
	{
		float randomRotation = (float)Mathf.DegToRad(GD.RandRange(-360, 360));
		return randomRotation;
	}

	public void RunMe()
	{
		var tween = GetTree().CreateTween();
		tween.SetLoops();
		tween.TweenCallback(Callable.From(SetRandomImage));
		tween.TweenProperty(this, "scale", SCALE_NORMAL, SCALE_TIME);
		tween.TweenProperty(this, "rotation", GetRandomRotation(), GetRandomSpinTime());
		tween.TweenProperty(this, "rotation", GetRandomRotation(), GetRandomSpinTime());
		tween.TweenProperty(this, "scale", SCALE_SMALL, SCALE_TIME);
		
	}
}
