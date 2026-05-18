using Godot;
using MedievalTDIncremental.Game.Enemies;
using MedievalTDIncremental.Game.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game {
	public enum VisibilityMode { 
		TWO_DIM,
		THREE_DIM
	}

	public abstract partial class CompositeNode<TNode2D, TNode3D>:Node
		where TNode2D: Node2D
		where TNode3D: Node3D
	{
		protected TNode2D Simulated { get; set; }
		protected TNode3D Model { get; set; }

		public Vector2 Position {
			get => Simulated.Position;
			set {
				this.Simulated.Position = value;
				this.Model.Position = new Vector3(value.X, 0, value.Y)/32; //potential issue with scaling
			}
		}

		private Vector2 _direction;
		public Vector2 Direction {
			get => _direction;
			set {
				_direction = value;
				Simulated.Rotation = value.Angle();
				Model.Basis = Basis.LookingAt(new(value.X, 0, value.Y));
			}
		}

		//todo: change this to use a global settings file instead
		public void SetVisibilityMode(VisibilityMode mode) {
			bool isTwoDim = mode == VisibilityMode.TWO_DIM;
			Simulated.Visible = isTwoDim;
			Model.Visible = !isTwoDim;
		}

		public override void _Ready() {
			base._Ready();
			Simulated = GetNode<TNode2D>("Simulated");
			Model = GetNode<TNode3D>("Model");
		}
	}
}
