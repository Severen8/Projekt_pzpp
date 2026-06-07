using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Towers;
[GlobalClass]
public partial class IgnoredMeshList: Resource {
	[Export]
	public string Prefix { get; set; }
	[Export]
	public string[] MeshNames { get; set; }

	public HashSet<MeshInstance3D> Meshes { get; set; }

	public IgnoredMeshList() { }


	public void ScrapeMeshes(Node3D model, string tier = "") {
		Meshes = [];
		for(int i=0; i<MeshNames.Length; i++){
			string meshPath = string.Concat(Prefix, tier, MeshNames[i]);
			var mesh = model.GetNodeOrNull<MeshInstance3D>(meshPath);
			if (mesh == null) {
				GD.PushWarning($"Did not fetch static mesh {model.GetPath().GetConcatenatedNames()}/{meshPath}");
				return;
			}
			Meshes.Add(mesh);
		}
	}



	public void SetMeshLock(bool lockState) {
		foreach(MeshInstance3D mesh in Meshes) {
			mesh.TopLevel = lockState;
		}
	}
}

