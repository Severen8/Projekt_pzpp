using Godot;
using MedievalTDIncremental.Game.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Towers;
[GlobalClass]
public partial class TowerSim: Node2D {
	public List<EnemySim> EnemiesInRange { get; set; }
	

	public TowerSim() { }

	public override void _Ready() {
		base._Ready();
		var range = GetNode<Area2D>("Area2D");
		range.BodyEntered += EnemyEnteredRange;
		range.BodyExited += EnemyLeftRange;
	}

	private void EnemyLeftRange(Node2D body) {
		this.EnemiesInRange.Remove((EnemySim) body);
	}

	private void EnemyEnteredRange(Node2D body) {
		this.EnemiesInRange.Add((EnemySim) body);
	}
}
