using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.PathLayer {
	public partial class PathLayer3D: GridMap {
		public override void _Ready() {
			this.SetCellItem(new Vector3I(0, 0, 0), 1);
		}

		public void InstantiateCells() { }
	}
}
