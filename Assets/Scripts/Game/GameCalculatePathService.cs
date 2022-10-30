using System.Collections.Generic;
using Core;
using Factories.Bug;
using Grid;
using Pathfinding;
using StaticData;
using UnityEngine;

namespace Game
{
	public class GameCalculatePathService : IService
	{
		private readonly IPathfindingService _pathfindingService;
		private readonly IStaticDataService _staticDataService;
		private readonly IGridService _gridService;
		private readonly Dictionary<int, Vector2Int[]> _bugsRoutesBuffer = new();


		public GameCalculatePathService(IPathfindingService pathfindingService, IStaticDataService staticDataService, 
			IGridService gridService)
		{
			_pathfindingService = pathfindingService;
			_staticDataService = staticDataService;
			_gridService = gridService;
		}

		public Dictionary<int, Vector2Int[]> Execute(List<Vector2Int> obstacles, Dictionary<int, IBugPresenter> bugsPresenterBuffer)
		{
			var gameRules = _staticDataService.GetGameRulesData();

			foreach (var hash in bugsPresenterBuffer.Keys)
			{
				var routes = 
					_pathfindingService.CalculatePath(gameRules.startPosition, gameRules.finishPosition, obstacles);

				DrawPathOnGrid(routes);
				
				if (_bugsRoutesBuffer.TryGetValue(hash, out _))
				{
					_bugsRoutesBuffer[hash] = routes;
					continue;
				}
				
				_bugsRoutesBuffer.Add(hash, routes);
			}

			return _bugsRoutesBuffer;
		}

		private void DrawPathOnGrid(Vector2Int[] route) => _gridService.DrawRoute(route);
	}
}