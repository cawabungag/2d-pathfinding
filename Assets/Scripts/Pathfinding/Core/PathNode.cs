using System;
using UnityEngine;
using Utils;

namespace Pathfinding.Core
{
	public readonly struct PathNode : IComparable<PathNode>
	{
		private readonly double _estimatedTotalCost;

		public PathNode(Vector2Int position, Vector2Int target, double traverseDistance)
		{
			Position = position;
			TraverseDistance = traverseDistance;
			var heuristicDistance = (position - target).DistanceEstimate();
			_estimatedTotalCost = traverseDistance + heuristicDistance;
		}

		public Vector2Int Position { get; }
		public double TraverseDistance { get; }

		public int CompareTo(PathNode other)
			=> _estimatedTotalCost.CompareTo(other._estimatedTotalCost);
	}
}