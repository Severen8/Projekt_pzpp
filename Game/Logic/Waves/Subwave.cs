using Godot;
using MedievalTDIncremental.Game.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MedievalTDIncremental.Game.Logic.Waves {
	[GlobalClass]
	public partial class Subwave : Resource, IWave {
		public event EventHandler<EnemySpawnArgs> EnemySpawned;
		public event EventHandler WaveEnded;

		[Export]
		public string EnemyType { get; set; }
		[Export]
		public int EnemyCount { get; set; } = 1;
		[Export]
		public float StartDelay { get; set; } = 0;
		[Export]
		public float BetweenDelay { get; set; } = 1;

		private int EnemiesRemaining { get; set; }

		public Subwave() { }


		public void StartWave() {
			EnemiesRemaining = EnemyCount;
			if (StartDelay > 0) {
				System.Timers.Timer startTimer = new(StartDelay*1000);
				startTimer.AutoReset = false;
				startTimer.Elapsed += StartTimerElapsed;
				startTimer.Start();
			} else {
				OnSubwaveStarted();
			}
		}

		private void StartTimerElapsed(object sender, ElapsedEventArgs e) {
			((System.Timers.Timer) sender).Dispose();
			OnSubwaveStarted();
		}


		private void OnSubwaveStarted() {
			System.Timers.Timer spawnTimer = new(BetweenDelay*1000);
			spawnTimer.AutoReset = false;
			spawnTimer.Elapsed += SpawnTimerElapsed;
			spawnTimer.Start();
			SpawnNextEnemy();
		}

		private void SpawnTimerElapsed(object sender, ElapsedEventArgs e) {
			SpawnNextEnemy();
			var spawnTimer = (System.Timers.Timer)sender;
			if (EnemiesRemaining > 0) {
				spawnTimer.Start();
			} else {
				spawnTimer.Dispose();
				WaveEnded.Invoke(this, new());
			}
		}

		private void SpawnNextEnemy() {
			EnemiesRemaining--;
			EnemySpawned.Invoke(this, new() { EnemyType = this.EnemyType });
		}
	}
}