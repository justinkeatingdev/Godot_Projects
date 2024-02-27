using Godot;
using System;
using System.Collections.Generic;

public partial class image_manager : Node
{
	List<Dictionary<string, string>> itemImages = new List<Dictionary<string, string>>();

	public override void _Ready()
	{
		LoadItemImages();
	}

	public void AddFileToList(string fn, string path)
	{
		var fullPath = path + "/" + fn;

		Dictionary<string, string> ii_dcit = new Dictionary<string, string> 
		{
			{ "item_name", fn.Replace(".png", "") },
			{ "item_texture", GD.Load(fullPath).ToString() }
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
}
