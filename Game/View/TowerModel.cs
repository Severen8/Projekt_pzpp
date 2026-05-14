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
		public int Tier { get; set; }


		public override void _Ready() {
			base._Ready();
			var children = GetChildren();
			for(int i=0; i<children.Count; i++) {
				Node3D model = (Node3D) children[i];
				model.Hide();
				IgnoredMeshes.ScrapeMeshes(model, (i+1).ToString());
				IgnoredMeshes.SetMeshLock(true);
			}
			SetTier(1);
		}

		
		protected void SetTier(int newTier) {
			CurrentModel.Hide();
			this.Tier = newTier;
			SetCurrentModel(newTier-1);
			CurrentModel.Show();
			PlayAnimation("upgrade");
		}
	}
}
