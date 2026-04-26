using Godot;
using MedievalTDIncremental.Game.Logic.Waves;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MedievalTDIncremental.Game.Logic {
	public partial class WaveHandler : Node2D, IWave {

		public event EventHandler<EnemySpawnArgs> EnemySpawned;
		public event EventHandler WaveEnded;

		//todo: maybe do this through dependency injection instead, so this doesn't have to be a node?
		[Export]
		public Wave[] Waves { get; set; }
		private int CurrentWave { get; set; }


		public override void _Ready() {
			CurrentWave = 0;
			//todo: add a start button maybe so it doesn't full send immediately
		}

		public void NextWave() {
			Waves[CurrentWave].WaveEnded += OnWaveEnded;
			Waves[CurrentWave].EnemySpawned += OnEnemySpawned;
			Waves[CurrentWave].StartWave();
		}

		//todo: move this up to Logic and use an EnemyHandler?
		private void OnEnemySpawned(object sender, EnemySpawnArgs e) {
			EnemySpawned.Invoke(this, e);
		}

		private void OnWaveEnded(object sender, EventArgs e) {
			Waves[CurrentWave].EnemySpawned -= OnEnemySpawned;
			Waves[CurrentWave].WaveEnded -= OnWaveEnded;
			Waves[CurrentWave++].Dispose();
			if(CurrentWave < Waves.Length)
				NextWave(); //todo: change this to have a potentially skippable delay, maybe handled by Logic.cs
			WaveEnded.Invoke(this, e);
		}

		public void StartWave() {
			throw new NotImplementedException();
		}
	}
}