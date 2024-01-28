using Godot;
using System;

public partial class place_cb : CharacterBody2D
{
	//public const float Speed = 300.0f;
	//public const float JumpVelocity = -400.0f;

	private AnimationPlayer player;
	private AnimatedSprite2D sprite;
	public Vector2 velocity;

	//[Signal]
	//public delegate void OnPlaneDiedEventHandler();


	//// Get the gravity from the project settings to be synced with RigidBody nodes.
	//public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public const float GRAVITY = 1900.0f;
	public const float POWER = -400.0f;

	public bool dead = false;

	public override void _Ready()
	{
		player = GetNode<AnimationPlayer>("AnimationPlayer");
		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{

		//// Add the gravity.
		//if (!IsOnFloor())
		velocity.Y += GRAVITY * (float)delta;

		Fly();



		//// Handle Jump.
		//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		//	velocity.Y = JumpVelocity;

		//// Get the input direction and handle the movement/deceleration.
		//// As good practice, you should replace UI actions with custom gameplay actions.
		//Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		//if (direction != Vector2.Zero)
		//{
		//	velocity.X = direction.X * Speed;
		//}
		//else
		//{
		//	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		//}

		
		MoveAndSlide();

		if (IsOnFloor())
		{
			Die();
		}
		Velocity = velocity;
	}

	public void Fly()
	{
		if (Input.IsActionJustPressed("fly"))
		{
			velocity.Y = POWER;			
			player.Play("fly");
		}
	}

	public void Die()
	{
		if (dead)
		{
			return;
		}
		dead = true;
		sprite.Stop();
		GameManager.gameManager.EmitOnGameOverSignal();
		SetPhysicsProcess(false);
	}
}
