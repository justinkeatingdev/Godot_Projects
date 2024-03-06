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

	public override void _Ready()
	{
		FrameImage = GetNode<TextureRect>("FrameImage");
		ItemImage = GetNode<TextureRect>("ItemImage");
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
	}
}
