using Godot;
using System;

public partial class GameScreen : Control
{
	public AudioStreamPlayer Sound;
	public TextureButton ExitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sound = GetNode<AudioStreamPlayer>("Sound");
		ExitButton = GetNode<TextureButton>("HB/MC2/VBoxContainer/ExitButton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_exit_button_pressed()
	{
		sound_manager.sound_Manager.PlayButtonClick(Sound);
		SingalManager.SignalManager.EmitOnGameExitPressed();
	}
}
