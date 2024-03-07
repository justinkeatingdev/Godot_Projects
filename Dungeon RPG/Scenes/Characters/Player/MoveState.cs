using DungeonRPG.Scripts;
using Godot;
using System;

public partial class MoveState : Node
{
	private Player characterNode;

	public override void _Ready()
	{
		characterNode = GetOwner<Player>();
		SetPhysicsProcess(false);
	}

	public override void _PhysicsProcess(double delta)
	{
		//GD.Print("Move ON");
		if (characterNode.direction == Vector2.Zero)
		{
			characterNode.stateMachineNode.SwitchState<IdleState>();
		}
	}

	public override void _Notification(int what)
	{
		base._Notification(what);

		if (what == 5001)
		{
			characterNode.animationPlayerNode.Play(GameConstants.ANIM_MOVE);
			SetPhysicsProcess(true);
		}
		else if (what == 5002)
		{
			SetPhysicsProcess(false);
		}
	}
}
