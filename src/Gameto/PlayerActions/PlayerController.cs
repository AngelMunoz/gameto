using Godot;
using System;
using Gameto.Lib;
using Gameto.Lib.Types;
using Gameto.Lib.Extensions;
using System.Threading.Tasks.Dataflow;
using System.Diagnostics;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 50f; // How fast the player will move (pixels/sec).

	private Vector2? _target = null;
	private AnimatedSprite2D? _animatedSprite = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Say.hello("From Godot + F#");
		_animatedSprite = GetNode<AnimatedSprite2D>("Movements");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("click"))
		{
			_target = GetGlobalMousePosition();
		}

		if (@event.IsMovingWithKeyboard())
		{
			_target = null;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PlayerMovement.animateKeyboardMovement(_animatedSprite.ToOption());
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_target is null || Position.DistanceTo(_target.Value) < 10)
		{
			PlayerMovement.moveWithKeyboard(this, Speed);
			_target = null;
			return;
		}

		PlayerMovement.moveWithMouse(this, _target.Value, Speed);
	}
}
