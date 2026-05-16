using Godot;
using MedievalTDIncremental.Game.View;
using System;
using System.Collections.Generic;

namespace MedievalTDIncremental.Game.Enemies {
	[GlobalClass]
	public partial class CompositeEnemy : CompositeNode {
		public event EventHandler<EnemyEscapeArgs> EnemyEscaped;
		public event EventHandler EnemyKilled;

		//todo: add more stats such as resource drops
		[ExportCategory("Enemy Stats")]
		[Export]
		public float Speed { get; set; } = 20;
		[Export]
		public int Damage { get; set; } = 10;

		List<Vector2> Path { get; set; }
		int PathIndex { get; set; }

		public override void _Ready() {
			base._Ready();
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