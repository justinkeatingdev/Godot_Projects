using Godot;
using System;
using System.Collections.Generic;

public partial class GameScreen : Control
{
	[Export]
	public PackedScene MemTileScene;

	public AudioStreamPlayer Sound;
	public TextureButton ExitButton;
	public GridContainer TileContainer;
	public Scorer ScorerNode;

	public Label MovesLabel;
	public Label PairsLabel;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sound = GetNode<AudioStreamPlayer>("Sound");
		ExitButton = GetNode<TextureButton>("HB/MC2/VBoxContainer/ExitButton");
		TileContainer = GetNode<GridContainer>("HB/MC1/TitleContainer");
		ScorerNode = GetNode<Scorer>("Scorer");

		MovesLabel = GetNode<Label>("HB/MC2/VBoxContainer/HB/MovesLabel");
		PairsLabel = GetNode<Label>("HB/MC2/VBoxContainer/HB2/PairsLabel");

		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnLevelSelected, new Callable(this, MethodName.OnLevelSelected));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MovesLabel.Text = Scorer.GetMovesMade();
		PairsLabel.Text = Scorer.GetPairsMade();
	}

	public void OnLevelSelected(int levelNum)
	{
		var levelSelection = game_manager.game_Manager.GetLevelSelection(levelNum);
		var frameImage = image_manager.image_Manager.GetRandomFrameImage();

		TileContainer.Columns = levelSelection.LdataCols;

		foreach (var iiDict in levelSelection.ImageList)
		{
			AddMemoryTitle(iiDict, frameImage);
		}

		ScorerNode.ClearNewGame(levelSelection.TargetPairs);
	}

	private void AddMemoryTitle(Dictionary<string, string> iiDict, CompressedTexture2D frameImage)
	{
		MemoryTile newTile = (MemoryTile)MemTileScene.Instantiate();
		TileContainer.AddChild(newTile);
		newTile.Setup(iiDict, frameImage);
	}

	public void _on_exit_button_pressed()
	{
		sound_manager.sound_Manager.PlayButtonClick(Sound);
		SingalManager.SignalManager.EmitOnGameExitPressed();
	}
}
