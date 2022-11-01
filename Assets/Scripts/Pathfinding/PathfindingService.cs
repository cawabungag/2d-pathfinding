using System.Collections.Generic;
using System.Linq;
using Pathfinding.Core;
using Unity.Burst;
using UnityEngine;

namespace Pathfinding
{
	public class PathfindingService : IPathfindingService
	{
		private readonly List<Vector2Int> _emptyObstacles = new();
		private const int MAX_STEPS = 100005;
		private readonly IPath _path;

		public PathfindingService()
			=> _path = new Path(MAX_STEPS);

		[BurstCompile]
		public Vector2Int[] CalculatePath(Vector2Int startPoint, Vector2Int finishPoint, List<Vector2Int> obstacles)
		{
			var obstaclesContainFinishPoint = obstacles.Contains(finishPoint);
			_path.Calculate(startPoint, finishPoint, obstaclesContainFinishPoint ? _emptyObstacles : obstacles,
				out var result);
			return result.Reverse().ToArray();
		}
	}
}