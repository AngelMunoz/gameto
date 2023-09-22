using Godot;
using System;
using Gameto.Lib;
using Gameto.Lib.Types;
using Gameto.Lib.Extensions;
using System.Threading.Tasks.Dataflow;
using System.Diagnostics;

namespace Gameto;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 50f; // How fast the player will move (pixels/sec).

	private Vector2? mouseTarget = null;
	private AnimatedSprite2D? playerSprite = null;

	private bool isRunning = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerSprite = GetNode<AnimatedSprite2D>("Movements");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("click"))
		{
			mouseTarget = GetGlobalMousePosition();
		}

		if (@event.IsMovingWithKeyboard())
		{
			mouseTarget = null;
		}

		if (@event.IsActionPressed("run"))
		{
			isRunning = true;
		}
		else if (@event.IsActionReleased("run"))
		{
			isRunning = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Player.AnimateKeyboardMovement(playerSprite.ToOption());
	}

	public override void _PhysicsProcess(double delta)
	{
		var speed = isRunning ? Speed * 2 : Speed;
		if (mouseTarget is null || Position.DistanceTo(mouseTarget.Value) < 10)
		{
			Player.MoveWithKeyboard(this, speed);
			mouseTarget = null;
			return;
		}


		Player.MoveWithMouse(this, mouseTarget.Value, speed);
	}
}
