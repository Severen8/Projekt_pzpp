using Godot;
using MedievalTDIncremental.Game.View;
using System;
using System.Collections.Generic;

namespace MedievalTDIncremental.Game.Enemies {
	[GlobalClass]
	public partial class CompositeEnemy : CompositeNode<EnemySim, AnimatableModel> {
		public delegate void EnemyHandler(CompositeEnemy sender);
		public event EnemyHandler EnemyEscaped;

		public event EnemyHandler EnemyKilled;

		//todo: add more stats such as resource drops
		[ExportCategory("Enemy Stats")]
		[Export]
		public float Speed { get; set; } = 20;
		[Export]
		public int Damage { get; set; } = 10;
		[Export]
		public float Health { get; set; } = 100;
		[Export]
		public float Coins { get; set; } = 10;

		public bool IsMoving { get; set; }

		EnemyPathfind Pathfinder { get; set; }
		
		

		public override void _Ready() {
			base._Ready();
			this.Instance2D.Damaged += OnDamaged;
			this.Pathfinder = new();
			this.Pathfinder.ReachedEnd += (s, e) => this.EnemyEscaped(this);
			this.Pathfinder.Turned += direction => this.Direction = direction;
			this.Direction = Pathfinder.DirectionToTarget;
		}



		public override void _PhysicsProcess(double delta) {
			base._PhysicsProcess(delta);
			if (IsMoving) {
				this.Position = Pathfinder.GetNextPos(delta * Speed);
				Instance3D.PlayAnimation("move");
			}
		}

		private void OnDamaged(float damage) {
			this.Health -= damage;
			if(Health <= 0) {
				this.Instance3D.PlayAnimation("death");
				IsMoving = false;
				this.Instance2D.Free();
				this.EnemyKilled.Invoke(this);
			}
		}
	}

}