using Godot;
using System;
using Gameto.Lib.Types;

namespace Gameto;
public partial class World : Node
{
	private TileMap? Map { get; set; }
	private PlayerController? Player { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Map = GetNode<TileMap>("TileMap");
		Player = GetNode<PlayerController>("Player");
	}

	public override void _PhysicsProcess(double delta)
	{
	}
}
