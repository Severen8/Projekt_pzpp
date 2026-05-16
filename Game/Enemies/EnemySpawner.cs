using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Enemies {
	public static class EnemySpawner {
		//todo: maybe move this into EnemyHandler?
		private static readonly Dictionary<string, Func<CompositeEnemy>> StringToEnemy = new() {
			
		};

		public static CompositeEnemy FromString(string EnemyType) {
			return StringToEnemy[EnemyType]();
		}
	}
}
