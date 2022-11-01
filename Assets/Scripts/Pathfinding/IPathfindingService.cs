using System.Collections.Generic;
using Core.Services;
using UnityEngine;

namespace Pathfinding
{
	public interface IPathfindingService : IService
	{
		Vector2Int[] CalculatePath(Vector2Int startPoint, Vector2Int finishPoint, List<Vector2Int> obstacles);
	}
}