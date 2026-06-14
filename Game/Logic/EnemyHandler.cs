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
			enemy.Position = Round.Singleton.Path[0];
			enemy.EnemyEscaped += OnEnemyEscaped;
			enemy.EnemyKilled += OnEnemyKilled;
		}

		void OnEnemyEscaped(CompositeEnemy sender) {
			sender.EnemyEscaped -= OnEnemyEscaped;
			EnemyDamagedPlayer.Invoke(sender.Damage);
			sender.QueueFree();
		}

		void OnEnemyKilled(CompositeEnemy sender) {
			var despawnTimer = new Timer();
			AddChild(despawnTimer);
			despawnTimer.Timeout += () => DespawnEnemy(sender, despawnTimer);
			despawnTimer.Start(2);
		}

		void DespawnEnemy(CompositeEnemy enemy, Timer despawnTimer) {
			enemy.QueueFree();
			despawnTimer.QueueFree();
			this.RemoveChild(enemy);
			this.RemoveChild(despawnTimer);
		}
	}
}
