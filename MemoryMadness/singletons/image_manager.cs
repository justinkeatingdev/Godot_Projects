using Godot;
using System;
using System.Collections.Generic;

public partial class image_manager : Node
{
	List<Dictionary<string, string>> itemImages = new List<Dictionary<string, string>>();
	public static image_manager image_Manager;

	public List<Godot.Resource> FRAME_IMAGES = new List<Godot.Resource>
	{
		GD.Load("res://assets/frames/blue_frame.png"),
		GD.Load("res://assets/frames/red_frame.png"),
		GD.Load("res://assets/frames/yellow_frame.png"),
		GD.Load("res://assets/frames/green_frame.png")
	};

	public override void _Ready()
	{
		image_Manager = this;
		LoadItemImages();
	}

	public void AddFileToList(string fn, string path)
	{
		var fullPath = path + "/" + fn;

		Dictionary<string, string> ii_dcit = new Dictionary<string, string> 
		{
			{ "item_name", fn.Replace(".png", "") },
			{ "item_texture", fullPath }
		};

		itemImages.Add(ii_dcit);
	}

	public void LoadItemImages()
	{
		var path = "res://assets/glitch";
		var dir = DirAccess.Open(path);

		if (dir == null)
		{
			GD.Print($"Error: {path}");
			return;
		}
		var fileNames = dir.GetFiles();

		//GD.Print(fileNames);

		foreach ( var file in fileNames )
		{
			if (!file.Contains(".import"))
			{
				AddFileToList(file, path);
			}
		}
	}

	public Dictionary<string, string> GetRandomItemImage()
	{
		var randomImage = GetRandomElement(itemImages);
		return randomImage;
	}

	private static T GetRandomElement<T>(List<T> list)
	{
		Random rand = new Random();
		int index = rand.Next(0, list.Count);
		return list[index];
	}

	private static void ShuffleList<T>(List<T> list)
	{
		Random random = new Random();

		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public Dictionary<string, string> GetImage(int index)
	{
		return itemImages[index];
	}

	public CompressedTexture2D GetRandomFrameImage()
	{
		CompressedTexture2D randomImage = (CompressedTexture2D)GetRandomElement(FRAME_IMAGES);
		return randomImage;
	}

	public void ShuffleImages()
	{
		ShuffleList(itemImages);
	}
}
