using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
	public class GridView : MonoBehaviour
	{
		[SerializeField] private int _width, _height;
 
		[SerializeField] private Tile _tilePrefab;
 
		private Dictionary<Vector2Int, Tile> _tiles;
 
		void Start() {
			GenerateGrid();
		}
 
		void GenerateGrid() {
			_tiles = new Dictionary<Vector2Int, Tile>();
			for (var x = 0; x < _width; x++) {
				for (var y = 0; y < _height; y++) {
					var spawnedTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity);
					spawnedTile.name = $"Tile {x} {y}";
					_tiles[new Vector2Int(x, y)] = spawnedTile;
				}
			}
 
			IPath path = new Path(100005);
			var asd = new Vector2Int[] {Vector2Int.down};
			path.Calculate(new Vector2Int(0,0), new Vector2Int(179, 89), asd, out var result);

			foreach (var rVector2Int in result)
			{
				var asdasdasd = GetTileAtPosition(rVector2Int);
				asdasdasd.SetPath();
			}
		}

		private Tile GetTileAtPosition(Vector2Int pos) {
			if (_tiles.TryGetValue(pos, out var tile)) return tile;
			return null;
		}
	}
}