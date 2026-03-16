using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public partial class PathLayer : TileMapLayer
{
	List<Vector2I> OrderedNodes { get; set; }
	List<Vector2I> Corners { get; set; }

	public override void _Ready() {
		base._Ready();
		OrderedNodes = [this.GetUsedCellsById(0, new Vector2I(0, 0), 0).First()];
		Corners = [.. OrderedNodes];
		this.ConstructGraph();
	}

	private void ConstructGraph() {
		HashSet<Vector2I> visited = new();
		Vector2I previous = this.OrderedNodes[0];
		Vector2I nextDir = this.GetSurroundingCells(previous).FirstOrDefault(previous) - previous;
		visited.Add(previous);

		while(nextDir != Vector2I.Zero) {
			previous = PopulateGraphInDirection(previous, nextDir);
			nextDir = this.GetSurroundingCells(previous)
				.Where(vec => vec != OrderedNodes[OrderedNodes.Count - 2] && this.GetCellSourceId(vec) != -1)
				.FirstOrDefault(previous)
				- previous;
			Corners.Add(previous);
		}
		foreach(Vector2I corner in Corners) {
			GD.Print(corner);
		}
	}

	private Vector2I PopulateGraphInDirection(Vector2I origin, Vector2I direction) {
		Vector2I next = origin;
		while(this.GetCellSourceId(next+=direction) != -1) {
			OrderedNodes.Add(next);
		}
		return next - direction;
	}
}
