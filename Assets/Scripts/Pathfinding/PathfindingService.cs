using System.Linq;
using Unity.Burst;
using UnityEngine;

namespace Pathfinding
{
	public class PathfindingService : IPathfindingService
	{
		private const int MAX_STEPS = 100005;
		private readonly IPath _path;

		public PathfindingService()
		{
			_path = new Path(MAX_STEPS);
		}

		[BurstCompile]
		public Vector2Int[] CalculatePath(Vector2Int startPoint, Vector2Int finishPoint, Vector2Int[] obstacles)
		{
			_path.Calculate(startPoint, finishPoint, obstacles, out var result);
			return result.Reverse().ToArray();
		}
	}
}