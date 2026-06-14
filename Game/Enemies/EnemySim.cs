using Godot;
using System;
using System.Collections.Generic;

namespace MedievalTDIncremental.Game.Enemies {
	[GlobalClass]
	public partial class EnemySim : AnimatableBody2D {
		public delegate void DamagedEventHandler(float damage);
		public event DamagedEventHandler Damaged;
		//todo: collision checks
		public override void _Ready() {
			base._Ready();
		}

		public void Damage(float damage) {
			this.Damaged.Invoke(damage);
		}
	}
}