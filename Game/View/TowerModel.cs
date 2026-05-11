using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.View {
	[GlobalClass]
	public partial class TowerModel: AnimatableModel {
		[Export]
		public IgnoredMeshList IgnoredMeshes { get; set; }

		public override void _Ready() {
			base._Ready();
			IgnoredMeshes.ScrapeMeshes(CurrentModel);
		}

		public override void RotateTowards(Vector2 simPos, Vector2 target) {
			base.RotateTowards(simPos, target);
			RevertStaticMeshRotation();
		}

		protected void RevertStaticMeshRotation() {
			foreach (MeshInstance3D mesh in IgnoredMeshes.Meshes) {
				Vector3 meshNewRotation = mesh.Rotation;
				meshNewRotation.Y = -Rotation.Y;
				mesh.Rotation = meshNewRotation;
			}
		}
	}
}
