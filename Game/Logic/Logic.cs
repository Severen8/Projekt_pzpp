using Godot;
using System;
using System.Collections.Generic;

public partial class Logic : Node2D
{
	static readonly PackedScene EnemyScene = ResourceLoader.Load<PackedScene>("res://Game/Logic/Enemy.tscn");

	List<Vector2> Path { get; set; }
	Enemy MonsterInstance { get; set; }
	
	public override void _Ready() {
		base._Ready();
		Path = GetNode<PathLayer>("PathLayer").GetVertexPath();
		MonsterInstance = EnemyScene.Instantiate<Enemy>();
		MonsterInstance.ConstructEnemy(this, Path);
	}
}
