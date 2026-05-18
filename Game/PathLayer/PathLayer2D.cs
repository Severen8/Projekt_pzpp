using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MedievalTDIncremental.Game.Logic.PathLayer {
	public partial class PathLayer2D : TileMapLayer {
		List<Vector2I> OrderedNodes { get; set; }
		List<Vector2I> Vertices { get; set; }

		public override void _Ready() {
			OrderedNodes = [GetUsedCellsById(0, new Vector2I(0, 0), 0).First()];
			Vertices = [.. OrderedNodes];
			ConstructGraph();
		}

		private void ConstructGraph() {
			HashSet<Vector2I> visited = new();
			Vector2I previous = OrderedNodes[0];
			Vector2I nextDir = GetSurroundingCells(previous).FirstOrDefault(previous) - previous;
			visited.Add(previous);

			while (nextDir != Vector2I.Zero) {
				previous = PopulateGraphInDirection(previous, nextDir);
				nextDir = GetSurroundingCells(previous)
					.Where(vec => vec != OrderedNodes[^2] && GetCellSourceId(vec) != -1)
					.FirstOrDefault(previous)
					- previous;
				Vertices.Add(previous);
			}
		}

		private Vector2I PopulateGraphInDirection(Vector2I origin, Vector2I direction) {
			Vector2I next = origin;
			while (GetCellSourceId(next += direction) != -1) {
				OrderedNodes.Add(next);
			}
			return next - direction;
		}

		public List<Vector2> GetVertexPath() {
			Vector2 TileOffset = new Vector2(this.TileSet.TileSize.X, this.TileSet.TileSize.Y) * 0.5f;
			return this.Vertices
				.Select(vert =>
					Position
					+ new Vector2(vert.X * this.TileSet.TileSize.X, vert.Y * this.TileSet.TileSize.Y)
					+ TileOffset)
				.ToList();
		}

		public (Vector2I Coords, Vector2I AtlasCoords)[] GetCellCoordPairs() =>
			GetUsedCells()
			.Select(coords => (coords, GetCellAtlasCoords(coords)))
			.ToArray();
	}
}