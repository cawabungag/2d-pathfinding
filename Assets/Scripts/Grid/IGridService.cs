using Core;
using Core.Services;
using Grid.Tile;
using UnityEngine;

namespace Grid
{
	public interface IGridService : IService 
	{
		// void GenerateGrid(int width, int height);
		void CreateTile(Vector2Int position);
		ITilePresenter GetTile(Vector2Int vector2Int);
		void DrawRoute(Vector2Int[] route);
		void Clear();
	}
}