using Godot;
using MedievalTDIncremental.Game;
using MedievalTDIncremental.Game.Towers;
using MedievalTDIncremental.Game.View;
using System;

[GlobalClass]
public partial class CompositeTower : CompositeNode<TowerSim, TowerModel> {
	[Export]
	public int Tier { get; set; }
	[Export]
	public TierData[] Tiers { get; set; }

	public CompositeTower() { }

	public void ChangeTiers(int newTier) {
		throw new NotImplementedException();
	}
}
