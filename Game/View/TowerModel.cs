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
			foreach(Node3D model in this.GetChildren()) {
				model.Hide();
				IgnoredMeshes.ScrapeMeshes(model, Tier.ToString());
				IgnoredMeshes.SetMeshLock(true);
			}
			SetTier(1);
		}



		public override void _Process(double delta) {
			base._Process(delta);
			if (Input.IsActionJustPressed("shoot_anim")) {
				PlayAnimation("shoot");
			}
			if (Input.IsActionJustPressed("reload_anim")) {
				SetTier(Tier + 1);
			}
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
