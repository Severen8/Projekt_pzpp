using Godot;
using MedievalTDIncremental.Game.Enemies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic {
	public partial class EnemyHandler : Node2D {
		public event EventHandler<EnemyEscapeArgs> EnemyEscaped;

		List<Vector2> Path { get; set; }
		public void SetPath(List<Vector2> path) {
			this.Path = path;
		}

		public void SpawnEnemy(string EnemyType) {
			CompositeEnemy enemy = EnemySpawner.FromString(EnemyType);
			enemy.StartPathfinding(Path);
			CallDeferred("add_child", enemy);
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
