using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public partial class Scorer : Node
{
	public AudioStreamPlayer Sound;
	public Timer RevealTimer;

	public Godot.Collections.Array<Node> Tiles;
	public List<MemoryTile> Selections = new List<MemoryTile>();
	public static int TargetPairs = 0;
	public static int MovesMade = 0;
	public static int PairsMade = 0;

	public override void _Ready()
	{
		Sound = GetNode<AudioStreamPlayer>("Sound");
		RevealTimer = GetNode<Timer>("RevealTimer");

		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnTileSelected, new Callable(this, MethodName.OnTileSelected));
		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnGameExitPressed, new Callable(this, MethodName.OnGameExitPressed));
	}

	public static string GetMovesMade()
	{
		return MovesMade.ToString();
	}

	public static string GetPairsMade()
	{
		return $"{PairsMade} / {TargetPairs}";
	}

	public void ClearNewGame(int targetPairs)
	{
		Selections.Clear();
		PairsMade = 0;
		MovesMade = 0;
		TargetPairs = targetPairs;
		Tiles = GetTree().GetNodesInGroup(game_manager.GROUP_TILE);
	}

	public bool SelectionsArePair()
	{
		bool isNotSameInstanceID = Selections[0].GetInstanceId() != Selections[1].GetInstanceId();
		var testerOne =  Selections.FirstOrDefault().ItemImage.Texture.ResourcePath;
		var testerTwo = Selections.LastOrDefault().ItemImage.Texture.ResourcePath;
		bool isPair = testerOne == testerTwo;

		bool isFinal = false;
		if (isNotSameInstanceID && isPair)
		{
			isFinal = true;
		}

		return isFinal;
	}

	public void KillTiles()
	{
		foreach (var s in Selections)
		{
			s.KillOnSuccess();
		}
		PairsMade += 1;
		sound_manager.sound_Manager.PlaySound(Sound, sound_manager.SOUND_SUCCESS);
	}

	public void UpdateSelections()
	{
		RevealTimer.Start();

		if (SelectionsArePair())
		{
			KillTiles();
		}
	}

	public void OnGameExitPressed()
	{
		RevealTimer.Stop();
	}

	public void CheckPairMade(MemoryTile tile)
	{
		tile.Reveal(true);
		Selections.Add(tile);

		if (Selections.Count() != 2)
		{
			return;
		}
		SingalManager.SignalManager.EmitOnSelectionDisabledSelected();
		MovesMade++;

		UpdateSelections();
	}

	public void OnTileSelected(MemoryTile tile)
	{
		sound_manager.sound_Manager.PlayTileClick(Sound);				
		CheckPairMade(tile);
	}

	public void _on_reveal_timer_timeout()
	{
		if (SelectionsArePair() == false)
		{
			HideSelections();
		}		
		Selections.Clear();
		CheckGameOver();
		SingalManager.SignalManager.EmitOnSelectionEnabled();
	}

	public void HideSelections()
	{
		foreach (var s in Selections)
		{
			s.Reveal(false);
		}
	}

	public void CheckGameOver()
	{
		if (PairsMade >= TargetPairs)
		{
			SingalManager.SignalManager.EmitOnGameOver(MovesMade);
		}
	}
}
