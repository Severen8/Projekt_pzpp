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
		public event EventHandler<EnemyEscapeArgs> EnemyEscaped;

		public void SpawnEnemy(string EnemyType) {
			CompositeEnemy enemy = EnemySpawner.FromString(EnemyType);
			this.AddChild(enemy);
			enemy.IsMoving = true;
			enemy.EnemyEscaped += OnEnemyEscaped;
		}

		void OnEnemyEscaped(Object s, EnemyEscapeArgs escapeArgs) {
			CompositeEnemy sender = (CompositeEnemy) s;
			sender.EnemyEscaped -= OnEnemyEscaped;
			EnemyEscaped.Invoke(this, escapeArgs);
			sender.QueueFree();
		}
	}
}
