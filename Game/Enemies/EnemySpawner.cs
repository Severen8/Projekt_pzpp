using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Enemies {
	public static class EnemySpawner {
		private const string ENEMIES_ROOT = "res://Game/Enemies/";
		private static readonly Dictionary<string, PackedScene> StringToEnemy;

		static EnemySpawner() {
			StringToEnemy = [];
			AddNewEnemy("Golem");
			AddNewEnemy("Slime");
			AddNewEnemy("Skeleton");
		}

		private static void AddNewEnemy(string enemyName) {
			StringToEnemy.Add(enemyName, ResourceLoader.Load<PackedScene>($"{ENEMIES_ROOT}{enemyName}/{enemyName}.tscn"));
		}


		public static CompositeEnemy FromString(string EnemyType) {
			return StringToEnemy[EnemyType].Instantiate<CompositeEnemy>();
		}
	}
}
