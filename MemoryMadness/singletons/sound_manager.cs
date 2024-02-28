using Godot;
using System;
using System.Collections.Generic;

public partial class sound_manager : Node
{

	public static sound_manager sound_Manager;
	public const string SOUND_MAIN_MENU = "SOUND_MAIN_MENU";
	public const string SOUND_IN_GAME = "SOUND_IN_GAME";
	public const string SOUND_SUCCESS = "SOUND_SUCCESS";
	public const string SOUND_GAME_OVER = "SOUND_GAME_OVER";
	public const string SOUND_SELECT_TILE = "SOUND_SELECT_TILE";
	public const string SOUND_SELECT_BUTTON = "SOUND_SELECT_BUTTON";

	public Dictionary<string, string> Sounds = new Dictionary<string, string> 
	{
		{ "SOUND_MAIN_MENU", "res://assets/sounds/bgm_action_3.mp3" },
		{ "SOUND_IN_GAME", "res://assets/sounds/bgm_action_4.mp3" },
		{ "SOUND_SUCCESS", "res://assets/sounds/sfx_sounds_fanfare3.wav" },
		{ "SOUND_GAME_OVER", "res://assets/sounds/sfx_sounds_powerup12.wav" },
		{ "SOUND_SELECT_TILE", "res://assets/sounds/sfx_sounds_impact1.wav" },
		{ "SOUND_SELECT_BUTTON", "res://assets/sounds/sfx_sounds_impact7.wav" }
	};

	public void PlaySound(AudioStreamPlayer player, string key)
	{
		if (Sounds.ContainsKey(key) == false)
		{
			return;
		}
		player.Stop();
		player.Stream = GD.Load<AudioStream>(Sounds[key]);
		player.Play();
	}

	public void PlayButtonClick(AudioStreamPlayer player)
	{
		PlaySound(player, SOUND_SELECT_BUTTON);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sound_Manager = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
