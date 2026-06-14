using Godot;
using MedievalTDIncremental.Game.Logic;
using MedievalTDIncremental.Game.Enemies;
using System;
using System.Collections.Generic;
using MedievalTDIncremental.Game;
using MedievalTDIncremental.Game.Logic.PathLayer;
using MedievalTDIncremental.Game.PathLayer;

public partial class Round : Node
{
	public static Round Singleton { get; private set; }

	public int Lives { get; private set; }
	public float Money { get; private set; }
	public List<Vector2> Path { get; private set; }
	public VisibilityMode VisMode { get; private set; } = VisibilityMode.THREE_DIM;

	WaveHandler WaveHandler { get; set; }
	EnemyHandler EnemyHandler { get; set; }
	TowerHandler TowerHandler { get; set; }

	public override void _EnterTree() {
		if (Singleton != null)
			Singleton.QueueFree();
		Singleton = this;
		Lives = 100;
	}

	public override void _Ready() {
		Path = GetNode<CompositePathLayer>("PathLayer").GetVertexPath();

		this.EnemyHandler = GetNode<EnemyHandler>("EnemyHandler");
		this.EnemyHandler.EnemyDamagedPlayer += TakeDamage;

		this.WaveHandler = GetNode<WaveHandler>("WaveHandler");
		//todo: potentially make a dedicated method instead of a delegate if i need to do something first
		//also this sucks and is dangerous if we change the method name, maybe use reflection?
		this.WaveHandler.EnemySpawned += (s, e) => EnemyHandler.CallDeferred("SpawnEnemy", e.EnemyType);
		this.WaveHandler.CallDeferred("StartWave");

		this.TowerHandler = new();
		this.AddChild(TowerHandler);
		TowerHandler.BuildTower(ResourceLoader.Load<PackedScene>("res://Game/Towers/Ballista/Ballista.tscn"), new Vector2(1, 8)*64+new Vector2(32, 32));
	}


	public void TakeDamage(int damage) {
		Lives = Math.Max(0, Lives-damage);
		GD.Print("Lives remaining: " + Lives);
		//todo: some kind of "You lost!" screen that pulls up the upgrade screen?
		//use a bool debounce to make sure you don't accidentally spam it
	}

	public bool TrySpendMoney(float value) {
		bool hasEnough = Money >= value;
		if (hasEnough) Money -= value;
		return hasEnough;
	}
}
