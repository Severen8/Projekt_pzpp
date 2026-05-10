using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.Waves
{
    public interface IWave
    {
		public event EventHandler WaveEnded;
		public event EventHandler<EnemySpawnArgs> EnemySpawned;
		public void StartWave();
	}
}
