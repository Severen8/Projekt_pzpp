using Godot;
using MedievalTDIncremental.Game.PathLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.PathLayer {
	[GlobalClass]
	public partial class CompositePathLayer : CompositeNode<TileMap2D, TileMap3D> {
		public List<Vector2> GetVertexPath() => Instance2D.GetVertexPath();

		public CompositePathLayer() { }

		public override void _Ready() {
			base._Ready();
			this.Instance3D.InstantiateTiles(this.Instance2D.GetTiles());
		}

		
	}
}