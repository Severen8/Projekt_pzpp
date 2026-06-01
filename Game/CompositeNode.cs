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
		protected TNode2D Instance2D { get; set; }
		protected TNode3D Instance3D { get; set; }

		public Vector2 Position {
			get => Instance2D.Position;
			set {
				this.Instance2D.Position = value;
				this.Instance3D.Position = new Vector3(value.X, 0, value.Y)/32; //potential issue with scaling
			}
		}

		private Vector2 _direction;
		public Vector2 Direction {
			get => _direction;
			set {
				_direction = value;
				Instance2D.Rotation = value.Angle();
				Instance3D.Basis = Basis.LookingAt(new(value.X, 0, value.Y));
			}
		}

		//todo: change this to use a global settings file instead
		public void SetVisibilityMode(VisibilityMode mode) {
			bool isTwoDim = mode == VisibilityMode.TWO_DIM;
			Instance2D.Visible = isTwoDim;
			Instance3D.Visible = !isTwoDim;
		}

		public override void _Ready() {
			base._Ready();
			Instance2D = GetNode<TNode2D>("Instance2D");
			Instance3D = GetNode<TNode3D>("Instance3D");
			this.SetVisibilityMode(Round.Singleton.VisMode);
		}
	}
}
