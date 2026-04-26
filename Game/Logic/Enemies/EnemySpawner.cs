using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.Enemies {
	public static class EnemySpawner {
		//todo: maybe move this into EnemyHandler?
		private static readonly Dictionary<string, Func<Enemy>> StringToEnemy = new() {
			{ "Enemy", () => Enemy.Load() }
		};

		public static Enemy FromString(string EnemyType) {
			return StringToEnemy[EnemyType]();
		}
	}
}
