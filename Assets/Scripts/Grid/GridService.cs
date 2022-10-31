using System.Collections.Generic;
using Factories.Tile;
using Grid.Tile;
using UnityEngine;

namespace Grid
{
	public class GridService : IGridService
	{
		private readonly ITileFactory _tileFactory;
		private readonly Dictionary<Vector2Int, ITilePresenter> _tiles = new();
		
		public GridService(ITileFactory tileFactory)
		{
			_tileFactory = tileFactory;
		}

		public void GenerateGrid(int width, int height)
		{
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					var position = new Vector2Int(x, y);
					CreateTile(position);
				}
			}
		}

		public void CreateTile(Vector2Int position)
		{
			var tile = _tileFactory.Create(position);
			_tiles.Add(position, tile);
		}

		public ITilePresenter GetTile(Vector2Int vector2Int) => _tiles[vector2Int];

		public void DrawRoute(Vector2Int[] route)
		{
			foreach (var point in route)
			{
				if (_tiles.TryGetValue(point, out var tile))
				{
					tile.SetPath();
				}
			}
		}

		public void Clear()
		{
			foreach (var tile in _tiles.Values) 
				tile.SetNormal();
		}
	}
}