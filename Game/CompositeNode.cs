using Godot;
using MedievalTDIncremental.Game.Enemies;
using MedievalTDIncremental.Game.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game {
	public partial class CompositeNode: Node {
		protected EnemySim Simulated { get; set; }
		protected AnimatableModel Model { get; set; }

		public Vector2 Position {
			get => Simulated.Position;
			set {
				this.Simulated.Position = value;
				this.Model.Position = new Vector3(value.X, 0, value.Y); //potential issue with scaling
			}
		}

		public override void _Ready() {
			base._Ready();
			Simulated = GetNode<EnemySim>("Simulated");
			Model = GetNode<AnimatableModel>("Model");
		}
	}
}
