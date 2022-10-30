using Assets;
using Grid.Tile;
using StaticData;
using UnityEngine;
using Utils;

namespace Factories.Tile
{
	public class TileFactory : ITileFactory
	{
		private readonly IInstantiator _instantiator;
		private readonly IStaticDataService _staticDataService;

		public TileFactory(IInstantiator instantiator, IStaticDataService staticDataService)
		{
			_instantiator = instantiator;
			_staticDataService = staticDataService;
		}
		
		public ITilePresenter Create(Vector2Int position)
		{
			var tilePath = _staticDataService.GetTilePath();
			var gameObject = _instantiator.InstantiateGameObject(tilePath, position.ToVector2F(), Quaternion.identity);
			var tileView = gameObject.GetComponent<TileView>();
			var presenter = new TilePresenter(tileView);
			presenter.SetNormal();
			return presenter;
		}
	}
}