using System.Collections.Generic;
using Bug;
using Core.Services;
using Pathfinding;
using StaticData;
using UI.Presenters;
using UnityEngine;
using Utils;

namespace Game
{
	public class GameCalculatePathService : IService
	{
		private const int SECOND_WAY_POINT_IN_ROUTE = 1;
		private readonly IPathfindingService _pathfindingService;
		private readonly IStaticDataService _staticDataService;
		private readonly PathPresenter _pathWindowPresenter;
		private readonly List<Vector2Int> _emptyObstacles = new();

		public GameCalculatePathService(IPathfindingService pathfindingService, IStaticDataService staticDataService,
			PathPresenter pathWindowPresenter)
		{
			_pathfindingService = pathfindingService;
			_staticDataService = staticDataService;
			_pathWindowPresenter = pathWindowPresenter;
		}

		public void Execute(List<Vector2Int> obstacles, List<IBugPresenter> bugsPresenterBuffer)
		{
			var gameRules = _staticDataService.GetGameRulesData();

			foreach (var bugPresenter in bugsPresenterBuffer)
			{
				var startPoint = bugPresenter.Position.ToVector2Int();
				var isBugInAvoidState = bugPresenter.CurrentState == BugState.Avoid;
				var routes =
					_pathfindingService.CalculatePath(startPoint, gameRules.finishPosition,
						isBugInAvoidState ? _emptyObstacles : obstacles);

				DrawPathOnPath(routes);
				bugPresenter.SetCurrentRoute(routes);
				bugPresenter.SetCurrentWayPoint(SECOND_WAY_POINT_IN_ROUTE);
			}
		}

		private void DrawPathOnPath(Vector2Int[] route) => _pathWindowPresenter.DrawPath(route);
	}
}