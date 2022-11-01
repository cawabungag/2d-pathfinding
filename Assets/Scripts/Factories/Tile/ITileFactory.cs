using Core.Factory;
using Core.Services;
using Grid.Tile;
using UnityEngine;

namespace Factories.Tile
{
	public interface ITileFactory : IFactory<Vector2Int, ITilePresenter>, IService { }
}