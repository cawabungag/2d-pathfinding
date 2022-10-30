using System.Collections.Generic;
using Core;
using Grid;
using InputService;
using StaticData;
using UnityEngine;
using Utils;

namespace Game
{
	public class GameObstaclesService : IService
	{
		private readonly IInputService _inputService;
		private readonly IGridService _gridService;
		private readonly IStaticDataService _staticDataService;

		private readonly List<Vector2Int> _obstaclesPointBuffer = new();

		public GameObstaclesService(IInputService inputService, IGridService gridService,
			IStaticDataService staticDataService)
		{
			_inputService = inputService;
			_gridService = gridService;
			_staticDataService = staticDataService;
		}

		public List<Vector2Int> Execute()
		{
			var mousePosition = _inputService.GetMousePosition;
			var mousePositionRound = mousePosition.ToVector2Int();
			var gameRules = _staticDataService.GetGameRulesData();
			var obstacleRadius = gameRules.obstacleRadius;
			var obstaclesPoints = GetObstaclesPoints(obstacleRadius, _obstaclesPointBuffer, 
				mousePositionRound);
			DrawObstaclesOnGrid(obstaclesPoints);
			return _obstaclesPointBuffer;
		}

		private void DrawObstaclesOnGrid(IEnumerable<Vector2Int> obstaclesPoints)
		{
			foreach (var obstaclesPoint in obstaclesPoints)
			{
				var tile = _gridService.GetTile(obstaclesPoint);
				tile?.SetObstacle();
			}
		}

		private IEnumerable<Vector2Int> GetObstaclesPoints(int obstacleRadius,
			ICollection<Vector2Int> obstaclesPointBuffer, Vector2Int mousePos)
		{
			obstaclesPointBuffer.Clear();
			var startX = mousePos.x - obstacleRadius;
			var startY = mousePos.y + obstacleRadius;
			var diameter = obstacleRadius * 2;
			for (var y = 0; y < diameter; y++)
			{
				for (var x = 0; x < diameter; x++)
				{
					var tilePosition = new Vector2Int(startX + x, startY - y);
					if (!InCircle(tilePosition, mousePos, obstacleRadius)) 
						continue;
					
					obstaclesPointBuffer.Add(tilePosition);
				}
			}
			return obstaclesPointBuffer;
		}
		
		private bool InCircle(Vector2 point, Vector2 circlePoint, float radius) {
			return (point - circlePoint).sqrMagnitude <= radius * radius;
		}
	}
}