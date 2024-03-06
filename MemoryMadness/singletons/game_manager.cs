using Godot;
using System;
using System.Collections.Generic;

public partial class game_manager : Node
{
	// Called when the node enters the scene tree for the first time.

	public const string GROUP_TILE = "tile";

	public static game_manager game_Manager;

	public static List<level> levels = new List<level> 
	{
		{new level(2, 2)},
		{new level(3, 4)},
		{new level(4, 4)},
		{new level(4, 6)},
		{new level(5, 6)},
		{new level(6, 6)}
	};

	public override void _Ready()
	{
		game_Manager = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public LevelSelection GetLevelSelection(int levelNum)
	{
		var lData = levels[levelNum];
		var numTiles = lData.Rows * lData.Cols;
		int targetPairs = numTiles / 2;
		List<Dictionary<string, string>> selectedLevelImages = new List<Dictionary<string, string>>();

		image_manager.image_Manager.ShuffleImages();

		for (int i = 0; i < targetPairs; i++) 
		{
			selectedLevelImages.Add(image_manager.image_Manager.GetImage(i));
			selectedLevelImages.Add(image_manager.image_Manager.GetImage(i));
		}

		ShuffleList(selectedLevelImages);

		LevelSelection levelSelection = new LevelSelection(targetPairs, lData.Cols, selectedLevelImages);

		return levelSelection;
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

	public void ClearNodesOfGroup(string gName)
	{
		foreach (var n in GetTree().GetNodesInGroup(gName))
		{
			n.QueueFree();
		}
	}
}

public class level
{
	public int Rows;
	public int Cols;

	public level(int rows, int cols)
	{
		Rows = rows;
		Cols = cols;
	}
}

public class LevelSelection
{
	public int TargetPairs;
	public int LdataCols;
	public List<System.Collections.Generic.Dictionary<string, string>> ImageList;

	public LevelSelection(int targetPairs, int lDataCols, List<System.Collections.Generic.Dictionary<string, string>> imageList)
	{
		TargetPairs = targetPairs;
		LdataCols = lDataCols;
		ImageList = imageList;
	}
}
