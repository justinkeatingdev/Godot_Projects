using Godot;
using System;

public partial class level_button : TextureButton
{
	public Label Label;
	public AudioStreamPlayer Sound;

	public int levelNumber = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label = GetNode<Label>("Label");
		Sound = GetNode<AudioStreamPlayer>("Sound");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetLevelNumber(int levelNum)
	{
		levelNumber = levelNum;
		level lData = game_manager.levels[levelNum];
		Label.Text = $"{lData.Rows}x{lData.Cols}";
	}

	public void _on_pressed()
	{
		sound_manager.sound_Manager.PlayButtonClick(Sound);
	}
}
