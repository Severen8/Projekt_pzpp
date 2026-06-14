using Godot;
using MedievalTDIncremental.Game.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Towers {
	[GlobalClass]
	public partial class TowerModel: AnimatableModel {
		[Export]
		public IgnoredMeshList IgnoredMeshes { get; set; }


		public override void _Ready() {
			base._Ready();
		}

		
		public void SetTier(TierData newTier) {
			var newModelScene = newTier.GetModel();
			Node3D model = newModelScene.Instantiate<Node3D>();
			this.AddChild(model);
			model.Basis = Basis.FlipZ;
			InitNewModel(model, newTier);
		}

		void InitNewModel(Node3D model, TierData newTier) {
			if (model.IsNodeReady()) {
				UpdateModelCache(model);
				this.IgnoredMeshes.ScrapeMeshes(CurrentModel, newTier.TierLevel.ToString());
				this.IgnoredMeshes.SetMeshLock(true);
			} else {
				model.Ready += () => InitNewModel(model, newTier);
			}
		}
	}
}
