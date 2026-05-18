using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic {
	[GlobalClass]
	public partial class CompositePathLayer: CompositeNode<PathLayer2D, PathLayer3D> {
		public CompositePathLayer() { }
	}
}
