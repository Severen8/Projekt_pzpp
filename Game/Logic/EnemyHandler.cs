using Godot;
using MedievalTDIncremental.Game.Enemies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic {
	public partial class EnemyHandler : Node {
		public delegate void DamageTakenHandler(int damage);
		public event DamageTakenHandler EnemyDamagedPlayer;

		public void SpawnEnemy(string EnemyType) {
			CompositeEnemy enemy = EnemySpawner.FromString(EnemyType);
			this.AddChild(enemy);
			enemy.IsMoving = true;
			enemy.EnemyEscaped += OnEnemyEscaped;
		}

		void OnEnemyEscaped(CompositeEnemy sender) {
			sender.EnemyEscaped -= OnEnemyEscaped;
			EnemyDamagedPlayer.Invoke(sender.Damage);
			sender.QueueFree();
		}
	}
}
