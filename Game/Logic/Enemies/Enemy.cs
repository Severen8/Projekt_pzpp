using Godot;
using System;
using System.Collections.Generic;

namespace MedievalTDIncremental.Game.Logic.Enemies {
	public partial class Enemy : AnimatableBody2D {
		public event EventHandler<EnemyEscapeArgs> EnemyEscaped;
		public event EventHandler EnemyKilled;

		//todo: add more stats such as hp drained upon escaping, resource drops and such
		//maybe a DTO resource? or maybe this can be one idk

		private static readonly PackedScene PackedScene = ResourceLoader.Load<PackedScene>("res://Game/Logic/Enemies/Enemy.tscn");
		
		const float Speed = 20; //todo: make this into a static value, change per enemy type
		public int Damage { get; private set; } = 10; 

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
				EnemyEscaped.Invoke(this, new() { Damage = this.Damage });
				return;
			}
			Position = Position.MoveToward(Path[PathIndex + 1], Speed * (float) delta);

			if (Position.IsEqualApprox(Path[PathIndex + 1]))
				PathIndex++;
		}
	}
}