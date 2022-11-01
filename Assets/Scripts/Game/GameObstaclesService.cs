using System.Collections.Generic;
using Core.Services;
using InputService;
using StaticData;
using UI.Presenters;
using UnityEngine;
using Utils;

namespace Game
{
	public class GameObstaclesService : IService
	{
		private readonly IInputService _inputService;
		private readonly IStaticDataService _staticDataService;
		private readonly CirclePresenter _circlePresenter;
		private readonly List<Vector2Int> _obstaclesPointBuffer = new();

		private int _radius;
		private Vector2Int _lastMousePosition;
		private int _lastObstacleRadius;

		public GameObstaclesService(IInputService inputService,
			IStaticDataService staticDataService, CirclePresenter circlePresenter)
		{
			_inputService = inputService;
			_staticDataService = staticDataService;
			_circlePresenter = circlePresenter;
		}

		public ObstacleData Execute()
		{
			var mousePosition = _inputService.GetMousePosition;
			var mousePositionRound = mousePosition.ToVector2Int();
			var gameRules = _staticDataService.GetGameRulesData();
			var obstacleRadius = gameRules.obstacleRadius;

			if (_radius != default)
				obstacleRadius = _radius;

			GetObstaclesPoints(obstacleRadius, _obstaclesPointBuffer, mousePositionRound);
			var isObstacleChanged = IsObstacleChanged(mousePositionRound, obstacleRadius);
			_lastMousePosition = mousePositionRound;
			_lastObstacleRadius = obstacleRadius;
			_circlePresenter.DrawCircle(mousePosition, obstacleRadius);
			var obstacleData = new ObstacleData(isObstacleChanged, obstacleRadius, _obstaclesPointBuffer, mousePosition);
			return obstacleData;
		}

		public void SetRadius(float radius)
		{
			_radius = Mathf.RoundToInt(radius);
		}

		private bool IsObstacleChanged(Vector2Int mousePositionRound, int obstacleRadius)
		{
			var isObstacleChanged = _lastMousePosition != mousePositionRound || _lastObstacleRadius != obstacleRadius;
			var isDefaultPosition = _lastMousePosition != default || _lastObstacleRadius != default;
			return isDefaultPosition || isObstacleChanged;
		}

		private void GetObstaclesPoints(int obstacleRadius,
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
					if (!VectorsExtensions.InCircle(tilePosition, mousePos, obstacleRadius))
						continue;

					obstaclesPointBuffer.Add(tilePosition);
				}
			}
		}


		public struct ObstacleData
		{
			public bool IsObstacleChanged { get; }
			public int Radius { get; }
			public List<Vector2Int> ObstaclesPointBuffer { get; }
			public Vector2 ObstaclePosition { get; }

			public ObstacleData(bool isObstacleChanged, int radius, List<Vector2Int> obstaclesPointBuffer,
				Vector2 obstaclePosition)
			{
				IsObstacleChanged = isObstacleChanged;
				Radius = radius;
				ObstaclesPointBuffer = obstaclesPointBuffer;
				ObstaclePosition = obstaclePosition;
			}
		}
	}
}