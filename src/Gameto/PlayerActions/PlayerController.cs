using Godot;
using System;
using Gameto.Lib;
using Gameto.Lib.Types;
using System.Threading.Tasks.Dataflow;
using System.Diagnostics;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 100f; // How fast the player will move (pixels/sec).

	private Vector2? _target = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Say.hello("From Godot + F#");
	}

	public override void _Input(InputEvent @event)
	{

		Debug.WriteLine($"Event: {@event.AsText()}");
		if (@event.IsActionPressed("click"))
		{
			_target = GetGlobalMousePosition();
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_target is null || Position.DistanceTo(_target.Value) < 10)
		{
			var direction = Input.GetVector("ui_left", "ui_right", "ui_down", "ui_up");
			Velocity = direction * Speed;
			MoveAndSlide();
			_target = null;
			return;
		}

		Velocity = Position.DirectionTo(_target.Value) * Speed;

		if (Position.DistanceTo(_target.Value) > 10)
		{
			MoveAndSlide();
		}
	}
}
