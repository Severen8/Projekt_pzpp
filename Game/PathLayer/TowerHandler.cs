using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.PathLayer;

public partial class TowerHandler : Node{
	public Dictionary<Vector2, CompositeTower> Towers { get; set; } = [];
	
	public void BuildTower(PackedScene tower, Vector2 position) {
		CompositeTower newTower = tower.Instantiate<CompositeTower>();
		if (Round.Singleton.TrySpendMoney(newTower.StarterCost)) {
			this.Towers[position] = newTower;
			this.AddChild(newTower);
			newTower.Init(position);
		} else {
			newTower.QueueFree();
		}
	}
}
