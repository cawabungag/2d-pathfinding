using Core;
using UnityEngine;

namespace Pathfinding
{
	public interface IPathfindingService : IService
	{
		Vector2Int[] CalculatePath(Vector2Int startPoint, Vector2Int finishPoint, Vector2Int[] obstacles);
	}
}