using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Core
{
	public interface IPath
	{
		void Calculate(Vector2Int start, Vector2Int target, IReadOnlyCollection<Vector2Int> obstacles,
			out IReadOnlyCollection<Vector2Int> path);
	}
}
