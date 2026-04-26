using Godot;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MedievalTDIncremental.Game.Logic.Waves {
	public partial class WaveHandler : Node2D {
		[Export]
		public Wave[] Waves { get; set; }

		private List<Vector2> Path { get; set; }
		private int CurrentWave { get; set; }
		Enemy LastEnemy { get; set; }


		public override void _Ready() {
			CurrentWave = 0;
			//todo: add a start button maybe so it doesn't full send immediately
		}

		public void Start(List<Vector2> path) {
			this.Path = path;
			NextWave();
		}

		public void NextWave() {
			Waves[CurrentWave].WaveEnded += OnWaveEnded;
			Waves[CurrentWave].EnemySpawned += OnEnemySpawned;
			Waves[CurrentWave].StartWave();
		}

		private void OnEnemySpawned(object sender, EnemySpawnArgs e) {
			e.SpawnedEnemy.Spawn(this, Path);
		}

		private void OnWaveEnded(object sender, EventArgs e) {
			Waves[CurrentWave].EnemySpawned -= OnEnemySpawned;
			Waves[CurrentWave].WaveEnded -= OnWaveEnded;
			Waves[CurrentWave++].Dispose();
			if(CurrentWave < Waves.Length)
				NextWave();
		}
	}
}