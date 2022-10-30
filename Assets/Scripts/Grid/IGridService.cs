using Core;
using Grid.Tile;
using UnityEngine;

namespace Grid
{
	public interface IGridService : IService 
	{
		void GenerateGrid(int width, int height);
		ITilePresenter GetTile(Vector2Int vector2Int);
	}
}