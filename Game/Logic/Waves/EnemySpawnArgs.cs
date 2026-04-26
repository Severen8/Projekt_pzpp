using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.Waves {
	public class EnemySpawnArgs : EventArgs{
		public required Enemy SpawnedEnemy { get; set; }
	}
}
