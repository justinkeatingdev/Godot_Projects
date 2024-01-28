using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

public partial class main : Node2D
{

	private Sprite2D icon2;

	public int health { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		icon2 = GetNode<Sprite2D>("Icon2");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		icon2.Position += new Vector2((float)delta * 50, 0f);
	}

}
