using Godot;
using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

public partial class animal : RigidBody2D
{

	readonly Vector2 DRAG_LIM_MAX = new Vector2(0, 60);
	readonly Vector2 DRAG_LIM_MIN = new Vector2(-60, 0);
	public static AudioStreamPlayer2D strechSound;
	public static AudioStreamPlayer2D launchSound;
	public static AudioStreamPlayer2D collisionSound;

	const float IMPULSE_MULT = 20.0f;
	const float FIRE_DELAY = 0.25f;
	const float STOPPED = 0.1f;

	private bool dead = false;
	private bool dragging = false;
	private bool released = false;
	private Vector2 start = Vector2.Zero;
	private Vector2 dragStart = Vector2.Zero;
	private Vector2 draggedVector = Vector2.Zero;
	private Vector2 lastDraggedPosition = Vector2.Zero;
	private float lastDragAmount = 0.0f;
	private float firedTime = 0.0f;
	private int lastCollisionCount = 0;

	public override void _Ready()
	{
		strechSound = GetNode<AudioStreamPlayer2D>("StretchSound");
		launchSound = GetNode<AudioStreamPlayer2D>("LaunchSound");
		collisionSound = GetNode<AudioStreamPlayer2D>("CollisionSound");
		start = GlobalPosition;
	}

	public override void _PhysicsProcess(double delta)
	{
		UpdateDebugLabel();

		if (released)
		{
			firedTime += (float)delta;
			if (firedTime > FIRE_DELAY)
			{
				PlayCollision();
				CheckOnTarget();
			}
		}
		else
		{
			if (dragging == false)
			{
				return;
			}
			else
			{
				if (Input.IsActionJustReleased("drag"))
				{
					ReleaseIt();
				}
				else
				{
					DragIt();
				}

			}
		}
	}

	public void UpdateDebugLabel()
	{
		var s = $"gPos: {Utils.VecToString(GlobalPosition)}\ncontacts: {GetContactCount()}";
		s += $"\ndragging: {dragging}\nreleased: {released}";
		s += $"\nstart: {start}\ndragStart: {Utils.VecToString(dragStart)}\ndraggedVector: {draggedVector}";
		s += $"\nlastDraggedPos: {lastDraggedPosition}\nlastDragAmount: {lastDragAmount}";
		s += $"\nang: {AngularVelocity}\nlinear: {Utils.VecToString(LinearVelocity)}\nfiredTime: {firedTime}";
		SignalManager.signalManager.EmitOnUpdateDebugSignal(s);
	}

	public void CheckOnTarget()
	{
		if (StoppedRolling() == false)
		{
			return;
		}

		var cb = GetCollidingBodies();

		if (cb.Count == 0)
		{
			return;
		}

		if (cb[0].IsInGroup(GameManager.GROUP_CUP))
		{
			GD.Print("CUP DIED");
			Die();
		}
	}

	public bool StoppedRolling()
	{
		if (GetContactCount() > 0)
		{
			if (Math.Abs(LinearVelocity.Y) < STOPPED && Math.Abs(AngularVelocity) < STOPPED)
			{
				return true;
			}
		}
		return false;
	}

	public void PlayCollision()
	{
		if (lastCollisionCount == 0 && GetContactCount() > 0 && collisionSound.Playing == false)
		{
			collisionSound.Play();
		}
		lastCollisionCount = GetContactCount();
	}

	public void GrabIt()
	{
		dragging = true;
		dragStart = GetGlobalMousePosition();
		lastDraggedPosition = dragStart;
	}

	public void DragIt()
	{
		var gmp = GetGlobalMousePosition();
		lastDragAmount = (lastDraggedPosition - gmp).Length();
		lastDraggedPosition = gmp;

		if (lastDragAmount > 0 && strechSound.Playing == false)
		{
			strechSound.Play();
		}

		draggedVector = gmp - dragStart;

		draggedVector.X = Mathf.Clamp(
			draggedVector.X,
			DRAG_LIM_MIN.X,
			DRAG_LIM_MAX.X
			);
		draggedVector.Y = Mathf.Clamp(
			draggedVector.Y,
			DRAG_LIM_MIN.Y,
			DRAG_LIM_MAX.Y
			);

		GlobalPosition = start + draggedVector;
	}

	public void ReleaseIt()
	{
		dragging = false;
		released = true;
		Freeze = false;
		ApplyCentralImpulse(GetImpulse());
		strechSound.Stop();
		launchSound.Play();

	}

	public Vector2 GetImpulse()
	{
		return draggedVector * -1 * IMPULSE_MULT;
	}

	public void Die()
	{
		if (dead)
		{
			return;
		}
		dead = true;
		SignalManager.signalManager.EmitOnAnimalDiedSignal();
		QueueFree();
	}

	public void _on_screen_exited()
	{
		Die();
	}

	public void _on_input_event(Node viewport, InputEvent eventVar, int shape_idx )
	{
		if(dragging || released)
		{
			return;
		}

		if (eventVar.IsActionPressed("drag"))
		{
			GrabIt();
		}
	}
}
