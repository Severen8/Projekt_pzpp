using Godot;
using System;
using System.Collections.Generic;

public partial class Enemy : AnimatableBody2D
{
	const float Speed = 20;
	List<Vector2> Path { get; set; }
	int PathIndex { get; set; }

	public override void _Ready() {
		base._Ready();
	}

	public void ConstructEnemy(Node parent, List<Vector2> path) {
		this.Position = path[0];
		this.Path = path;
		parent.AddChild(this);
	}

	
	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		if (PathIndex >= Path.Count - 1) {
			Free();
			return;
		}
		Position = Position.MoveToward(Path[PathIndex + 1], Speed*(float)delta);

		if (Position.IsEqualApprox(Path[PathIndex + 1]))
			PathIndex++;
	}
}
