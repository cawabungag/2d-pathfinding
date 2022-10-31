using System.Collections.Generic;
using Bug;
using Core;
using Core.Services;
using Pathfinding;
using StaticData;
using UnityEngine;
using Utils;

namespace Game
{
	public class GameCalculatePathService : IService
	{
		private const int SECOND_WAY_POINT_IN_ROUTE = 1;
		private readonly IPathfindingService _pathfindingService;
		private readonly IStaticDataService _staticDataService;
		
		public GameCalculatePathService(IPathfindingService pathfindingService, IStaticDataService staticDataService)
		{
			_pathfindingService = pathfindingService;
			_staticDataService = staticDataService;
		}

		public void Execute(List<Vector2Int> obstacles, List<IBugPresenter> bugsPresenterBuffer)
		{
			var gameRules = _staticDataService.GetGameRulesData();

			foreach (var bugPresenter in bugsPresenterBuffer)
			{
				var startPoint = bugPresenter.Position.ToVector2Int();
				var routes = 
					_pathfindingService.CalculatePath(startPoint, gameRules.finishPosition, obstacles);

				// DrawPathOnGrid(routes);
				bugPresenter.SetCurrentRoute(routes);
				bugPresenter.SetCurrentWayPoint(SECOND_WAY_POINT_IN_ROUTE);
			}
		}

		// private void DrawPathOnGrid(Vector2Int[] route) => _gridService.DrawRoute(route);
	}
}