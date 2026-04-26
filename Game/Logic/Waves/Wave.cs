using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.Waves {
	[GlobalClass]
	public partial class Wave : Resource, IWave {
		public event EventHandler WaveEnded;
		public event EventHandler<EnemySpawnArgs> EnemySpawned;

		[Export]
		public Subwave[] Subwaves { get; set; }
		private HashSet<Subwave> Ongoing { get; set; }

		public Wave() { }

		public void StartWave() {
			Ongoing = new();
			foreach(Subwave subwave in Subwaves) { 
				Ongoing.Add(subwave);
				subwave.EnemySpawned += (s, e) => EnemySpawned.Invoke(this, e);
				subwave.WaveEnded += OnSubwaveEnded;
				subwave.StartWave();
			}
		}

		private void OnSubwaveEnded(object subwave, EventArgs e) {
			Ongoing.Remove((Subwave)subwave);
			((Subwave)subwave).Dispose();
			if (Ongoing.Count == 0)
				WaveEnded.Invoke(this, new());
		}
	}
}

