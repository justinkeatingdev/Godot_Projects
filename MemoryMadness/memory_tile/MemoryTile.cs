using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class MemoryTile : TextureButton
{
	// Called when the node enters the scene tree for the first time.

	public TextureRect FrameImage;
	public TextureRect ItemImage;

	public string ItemImageName;
	bool canSelectMe = true;

	public override void _Ready()
	{
		FrameImage = GetNode<TextureRect>("FrameImage");
		ItemImage = GetNode<TextureRect>("ItemImage");

		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnSelectionDisabled, new Callable(this, MethodName.OnSelectionDisabled));
		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnSelectionEnabled, new Callable(this, MethodName.OnSelectionEnabled));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public string GetItemName()
	{
		return ItemImageName;
	}

	public void Setup(System.Collections.Generic.Dictionary<string, string> iiDict, CompressedTexture2D fi)
	{
		FrameImage.Texture = fi;

		var fullPath = iiDict.LastOrDefault().Value;
		var texture = GD.Load<Texture2D>(fullPath);		
		ItemImage.Texture = texture;
		Reveal(false);
	}

	public void OnSelectionDisabled()
	{
		canSelectMe = false;
	}

	public void KillOnSuccess()
	{
		ZIndex = 1;
		var tween = GetTree().CreateTween();
		tween.SetParallel(true);
		tween.TweenProperty(this, "disabled", true, 0);
		tween.TweenProperty(this, "rotation", DegreeToRadians(720), 0.5);
		tween.TweenProperty(this, "scale", new Vector2(1.5f, 1.5f), 0.5);
		tween.SetParallel(false);
		tween.TweenInterval(0.6);
		tween.TweenProperty(this, "scale", new Vector2(0f, 0f), 0);
	}

	public void OnSelectionEnabled()
	{
		canSelectMe = true;
	}

	public void Reveal(bool r)
	{
		FrameImage.Visible = r;
		ItemImage.Visible = r;
	}

	public void _on_pressed()
	{
		if (canSelectMe)
		{
			SingalManager.SignalManager.EmitOnTileSelected(this);
		}
	}

	public double DegreeToRadians(double degrees)
	{
		double radians = degrees * Math.PI / 180;

		return radians;
	}
}
