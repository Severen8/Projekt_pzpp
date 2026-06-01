using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.PathLayer;
public struct Tile3D {
	public Tile3D(int tileType, Vector3I offset) {
		this.TileType = tileType;
		this.Offset = offset;
	}
	public int TileType { get; set; }
	public Vector3I Offset { get; set; }
}
public struct Tile2D {
	public Tile2D(Vector2I tileCoords, Vector2I atlasCoords) {
		this.TileCoords = tileCoords;
		this.AtlasCoords = atlasCoords;
	}
	public Vector2I TileCoords { get; set; }
	public Vector2I AtlasCoords { get; set; }
}