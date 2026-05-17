using Godot;
using MedievalTDIncremental.Game.View;
using System;
using System.Collections.Generic;

namespace MedievalTDIncremental.Game.Enemies {
	[GlobalClass]
	public partial class CompositeEnemy : CompositeNode {
		public delegate void EnemyEscapedHandler(CompositeEnemy sender);
		public event EnemyEscapedHandler EnemyEscaped;

		public event EventHandler EnemyKilled;

		//todo: add more stats such as resource drops
		[ExportCategory("Enemy Stats")]
		[Export]
		public float Speed { get; set; } = 20;
		[Export]
		public int Damage { get; set; } = 10;

		public bool IsMoving { get; set; }

		EnemyPathfind Pathfinder { get; set; }
		
		

		public override void _Ready() {
			base._Ready();
			this.Pathfinder = new();
			this.Pathfinder.ReachedEnd += (s, e) => this.EnemyEscaped(this);
			this.Pathfinder.Turned += angle => this.Rotation = angle;
			this.Rotation = Pathfinder.AngleToTarget;
		}



		public override void _PhysicsProcess(double delta) {
			base._PhysicsProcess(delta);
			if (IsMoving) {
				this.Position = Pathfinder.GetNextPos(delta * Speed);
				Model.PlayAnimation("move");
			}
		}
	}

}