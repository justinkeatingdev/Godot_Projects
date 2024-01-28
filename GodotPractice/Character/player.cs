using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class player : CharacterBody2D
{
	[Export]
	public static float Speed = 200.0f;
	[Export]
	public static float JumpVelocity = -200.0f;
	[Export]
	public static float DoubleJump = -100.0f;

	public static AnimatedSprite2D animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public static bool hasJumped = false;
	public static bool animationLocked = false;
	public static Vector2 direction = Vector2.Zero;
	public static Vector2 velocity = Vector2.Zero;
	public static bool wasInAir = false;

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
			wasInAir = true;
		}
		else
		{
			hasJumped = false;
		}		

		if (wasInAir)
		{
			land();
			wasInAir = false;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump")){
			if (IsOnFloor()){
				Jump();				
			}
			else if (!hasJumped){
				velocity.Y = DoubleJump;
				hasJumped = true;
			}
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;

		MoveAndSlide();
		UpdateAnimation();
		UpdateFacingDirection();
	}

	private void land()
	{
		animatedSprite.Play("jump_end");
		animationLocked = true;
	}

	private void UpdateFacingDirection()
	{
		if (direction.X > 0)
		{
			animatedSprite.FlipH = false;
		}
		else if (direction.X < 0)
		{
			animatedSprite.FlipH = true;
		}
	}

	public void UpdateAnimation()
	{
		if (!animationLocked){
			if (direction.X != 0)
			{
				animatedSprite.Play("run");
			}
			else
			{
				animatedSprite.Play("idle");
			}
		}
	}

	public void Jump()
	{
		velocity.Y = JumpVelocity;
		animationLocked = true;
	}

	private void _on_animated_sprite_2d_animation_finished()
	{
		// Replace with function body.
		if (animatedSprite.Animation == "jump_end")
		{
			animationLocked = false;
		}
	}
}
