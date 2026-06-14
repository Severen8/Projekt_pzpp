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
	double Cooldown { get; set; } = 0;
	TierData Stats { get; set; }
	Area2D Range { get; set; }
	EnemySim CurrentTarget =>
		EnemiesInRange.Count > 0 ? EnemiesInRange[0] : null;


	List<EnemySim> EnemiesInRange { get; set; } = [];

	public delegate void ShootEventHandler();
	public event ShootEventHandler Shoot;

	public TowerSim() { }

	

	public override void _Ready() {
		base._Ready();
		Range = GetNode<Area2D>("Area2D");
		Range.AreaEntered += EnemyEnteredRange;
		Range.AreaExited += EnemyLeftRange;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		if(Stats != null && CurrentTarget != null) {
			TryShoot(delta);
		}
	}

	private void TryShoot(double delta) {
		if(this.Cooldown > 0) {
			this.Cooldown -= delta;
		} else if (!CurrentTarget.IsQueuedForDeletion()){
			Shoot.Invoke();
			CurrentTarget.Damage(this.Stats.Damage);
			this.Cooldown += this.Stats.Cooldown;
		} else {
			EnemiesInRange.Remove(CurrentTarget);
		}
	}



	private void EnemyLeftRange(Node2D area) {
		this.EnemiesInRange.Remove((EnemySim) area.GetParent());
	}

	private void EnemyEnteredRange(Node2D area) {
		this.EnemiesInRange.Add((EnemySim) area.GetParent());
	}

	public void SetTier(TierData newTier) {
		this.Stats = newTier;
		this.Cooldown = newTier.Cooldown;
		this.Range.Scale = new Vector2(Stats.Range, Stats.Range);

		var texture = new ImageTexture();
		texture.SetImage(newTier.GetSprite());
		this.GetNode<Sprite2D>("Sprite2D").Texture = texture;
	}

	public bool TryGetTargetVector(out Vector2 lookDir) {
		bool hasTarget = CurrentTarget != null;
		lookDir = hasTarget ? Position.DirectionTo(CurrentTarget.Position) : Vector2.Zero;
		return hasTarget;
	}
}
