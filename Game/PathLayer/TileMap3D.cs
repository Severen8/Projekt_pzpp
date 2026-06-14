using Godot;
using MedievalTDIncremental.Game.PathLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalTDIncremental.Game.Logic.PathLayer;
public partial class TileMap3D: GridMap {
	//replace this with multiple pathlayers instead for decor and for the ground
	static readonly Dictionary<Vector2I, Tile3D> TileDictionary = new(){
		{new(0,0), new(0, Vector3I.Down) },
		{new(1,0), new(2, Vector3I.Down) },
		{new(2,0), new(1, Vector3I.Down) },
		{new(0,1), new(5, Vector3I.Zero) },
		{new(1,1), new(6, Vector3I.Zero) },
		{new(2,1), new(3, Vector3I.Zero) },
		{new(0,2), new(7, Vector3I.Zero) },
		{new(1,2), new(8, Vector3I.Up  ) },
		{new(2,2), new(4, Vector3I.Zero) }
	};

	public override void _Ready() {
		
	}

	public void InstantiateTiles(List<Tile2D> tiles) {
		foreach(Tile2D tile in tiles) {
			Tile3D equivalent = TileDictionary[tile.AtlasCoords];
			Vector3I newPosition = new Vector3I(tile.TileCoords.X, 0, tile.TileCoords.Y) + equivalent.Offset;
			this.SetCellItem(newPosition, equivalent.TileType);
		}
	}
}
