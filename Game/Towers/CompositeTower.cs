using Godot;
using MedievalTDIncremental.Game;
using MedievalTDIncremental.Game.Towers;
using MedievalTDIncremental.Game.View;
using System;

[GlobalClass]
public partial class CompositeTower : CompositeNode<TowerSim, TowerModel> {
	public int Tier { get; private set; }
	[Export]
	public TierData[] Tiers { get; set; }
	public float StarterCost => Tiers[0].Cost;

	public CompositeTower() { }

	public override void _Ready() {
		base._Ready();
		this.Instance2D.Shoot += () => Instance3D.PlayAnimation("shoot");
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Vector2 newLookDir;
		if(Instance2D.TryGetTargetVector(out newLookDir)) {
			this.Direction = newLookDir;
		}
	}

	public void SetTier(int newTier) {
		this.Instance3D.SetTier(Tiers[newTier-1]);
		this.Instance2D.SetTier(Tiers[newTier-1]);
		this.Tier = newTier;
	}

	public void Init(Vector2 position) {
		if (this.IsNodeReady()) {
			this.Position = position;
			this.SetTier(1);
		} else {
			this.Ready += () => Init(position);
		}
	}
}
