using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Towers;
[GlobalClass]
public partial class TierData: Resource{
	[Export]
	public int TierLevel { get; set; }
	[Export]
	public float Cost { get; set; }
	[Export]
	public float Damage { get; set; }
	[Export]
	public float Range { get; set; }
	[Export]
	public float Cooldown { get; set; }

	public TierData() { }

	public PackedScene GetModel() {
		string path = this.ResourcePath.GetBaseDir() + "/Models/T" + TierLevel + ".fbx";
		return ResourceLoader.Load<PackedScene>(path);
	}

	public Image GetSprite() {
		string path = this.ResourcePath.GetBaseDir() + "/Sprites/T" + TierLevel + ".png";
		return Image.LoadFromFile(path);
	}
}
