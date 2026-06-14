using Godot;
using MedievalTDIncremental.Game.Logic.PathLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.PathLayer;
public partial class TileMap2D : Node2D {
	public List<Vector2> GetVertexPath() => PathLayer.GetVertexPath();
	private PathLayer2D PathLayer { get; set; }

	public override void _Ready() {
		base._Ready();
		this.PathLayer = GetNode<PathLayer2D>("Terrain");
	}
	
	public List<Tile2D> GetTiles() {
		List<Tile2D> tiles = [];
		foreach(TileMapLayer tilemap in GetChildren()) {
			tiles.AddRange(tilemap
				.GetUsedCells()
				.Select(tileCoords => new Tile2D(tileCoords, tilemap.GetCellAtlasCoords(tileCoords))));
		}
		return tiles;
	}
}
