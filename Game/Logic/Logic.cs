using Godot;
using MedievalTDIncremental.Game.Logic;
using MedievalTDIncremental.Game.Logic.Enemies;
using System;
using System.Collections.Generic;

public partial class Logic : Node2D
{
	public List<Vector2> Path { get; set; }
	public int Lives { get; set; } = 100; //todo: extract this to another class later if needed

	WaveHandler WaveHandler { get; set; }
	EnemyHandler EnemyHandler { get; set; }
	
	public override void _Ready() {
		Path = GetNode<PathLayer>("PathLayer").GetVertexPath();
		this.EnemyHandler = GetNode<EnemyHandler>("EnemyHandler");
		this.EnemyHandler.SetPath(Path);
		this.EnemyHandler.EnemyEscaped += OnEnemyEscaped;

		this.WaveHandler = GetNode<WaveHandler>("WaveHandler");
		//todo: potentially make a dedicated method instead of a delegate if i need to do something first
		this.WaveHandler.EnemySpawned += (s, e) => this.EnemyHandler.SpawnEnemy(e.EnemyType);
		this.WaveHandler.CallDeferred("NextWave");
	}


	void OnEnemyEscaped(Object s, EnemyEscapeArgs escapeArgs) {
		Lives = Math.Max(0, Lives-escapeArgs.Damage);
		GD.Print("Lives remaining: " + Lives);
		//todo: some kind of "You lost!" screen that pulls up the upgrade screen?
		//use a bool debounce to make sure you don't accidentally spam it
	}
}
