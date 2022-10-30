using Core;
using Core.Factory;
using Grid.Tile;
using UnityEngine;

namespace Factories.Tile
{
	public interface ITileFactory : IFactory<Vector2Int, ITilePresenter>, IService { }
}