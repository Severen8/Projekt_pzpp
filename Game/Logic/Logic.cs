using Godot;
using MedievalTDIncremental.Game.Logic;
using System;
using System.Collections.Generic;

public partial class Logic : Node2D
{
	public List<Vector2> Path { get; set; }
	WaveHandler WaveHandler { get; set; }
	EnemyHandler EnemyHandler { get; set; }
	
	public override void _Ready() {
		Path = GetNode<PathLayer>("PathLayer").GetVertexPath();
		this.EnemyHandler = GetNode<EnemyHandler>("EnemyHandler");
		this.EnemyHandler.SetPath(Path);

		this.WaveHandler = GetNode<WaveHandler>("WaveHandler");
		//todo: potentially make a dedicated method instead of a delegate if i need to do something first
		this.WaveHandler.EnemySpawned += (s, e) => this.EnemyHandler.SpawnEnemy(e.EnemyType);
		this.WaveHandler.CallDeferred("NextWave");
		GD.Print("logic done");
	}
}
