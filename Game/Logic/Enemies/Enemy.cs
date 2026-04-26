using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : AnimatableBody2D
{
	private static readonly PackedScene PackedScene = ResourceLoader.Load<PackedScene>("res://Game/Logic/Enemies/Enemy.tscn");
	const float Speed = 20;
	List<Vector2> Path { get; set; }
	int PathIndex { get; set; }


	public static Enemy Load() {
		return PackedScene.Instantiate<Enemy>();
	}



	public void StartPathfinding(List<Vector2> path) {
		this.Position = path[0];
		this.Path = path;
	}

	
	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		if (PathIndex >= Path.Count - 1) {
			Free(); //todo: fire event instead, handle this in the parent
			return;
		}
		Position = Position.MoveToward(Path[PathIndex + 1], Speed*(float)delta);

		if (Position.IsEqualApprox(Path[PathIndex + 1]))
			PathIndex++;
	}
}
