using Godot;
using MedievalTDIncremental.Game.Logic;
using MedievalTDIncremental.Game.Enemies;
using System;
using System.Collections.Generic;
using MedievalTDIncremental.Game;
using MedievalTDIncremental.Game.Logic.PathLayer;

public partial class Round : Node
{
	public static Round Singleton { get; private set; }

	public int Lives { get; private set; }
	public List<Vector2> Path { get; private set; }

	WaveHandler WaveHandler { get; set; }
	EnemyHandler EnemyHandler { get; set; }
	
	public override void _Ready() {
		if (Singleton != null)
			Singleton.QueueFree();
		Singleton = this;
		Lives = 100;
		Path = GetNode<CompositePathLayer>("PathLayer").GetVertexPath();

		this.EnemyHandler = GetNode<EnemyHandler>("EnemyHandler");
		this.EnemyHandler.EnemyDamagedPlayer += TakeDamage;

		this.WaveHandler = GetNode<WaveHandler>("WaveHandler");
		//todo: potentially make a dedicated method instead of a delegate if i need to do something first
		//also this sucks and is dangerous if we change the method name, maybe use reflection?
		this.WaveHandler.EnemySpawned += (s, e) => EnemyHandler.CallDeferred("SpawnEnemy", e.EnemyType);
		this.WaveHandler.CallDeferred("StartWave");
	}


	public void TakeDamage(int Damage) {
		Lives = Math.Max(0, Lives-Damage);
		GD.Print("Lives remaining: " + Lives);
		//todo: some kind of "You lost!" screen that pulls up the upgrade screen?
		//use a bool debounce to make sure you don't accidentally spam it
	}
}
