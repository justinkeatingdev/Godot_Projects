using Godot;
using System;

public partial class master_scene : CanvasLayer
{

	public Control MainScreen;
	public Control GameScreen;
	public AudioStreamPlayer Sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		MainScreen = GetNode<Control>("MainScreen");
		GameScreen = GetNode<Control>("GameScreen");
		Sound = GetNode<AudioStreamPlayer>("Sound");

		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnGameExitPressed, new Callable(this, MethodName.OnGameExitPressed));
		SingalManager.SignalManager.Connect(SingalManager.SignalName.OnLevelSelected, new Callable(this, MethodName.OnLevelSelected));

		OnGameExitPressed();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ShowGame(bool s)
	{
		GameScreen.Visible = s;
		MainScreen.Visible = !s;
	}

	public void OnGameExitPressed()
	{
		ShowGame(false);
		sound_manager.sound_Manager.PlaySound(Sound, sound_manager.SOUND_MAIN_MENU);
	}

	public void OnLevelSelected(int levelNum)
	{
		ShowGame(true);
		sound_manager.sound_Manager.PlaySound(Sound, sound_manager.SOUND_MAIN_MENU);
	}
}
