using Godot;
using MedievalTDIncremental.Game.Logic.Waves;
using System;
using System.Collections.Generic;

public partial class Logic : Node2D
{
	public List<Vector2> Path { get; set; }
	Enemy MonsterInstance { get; set; }
	
	public override void _Ready() {
		Path = GetNode<PathLayer>("PathLayer").GetVertexPath();
		GetNode<WaveHandler>("WaveHandler").Start(Path);
		GD.Print("logic done");
	}
}
