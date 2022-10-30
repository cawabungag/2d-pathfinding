using System;
using UnityEngine;

namespace Utils
{
	public static class Vector2IntExtensions
	{
		public static double DistanceEstimate(this Vector2Int vector2Int)
		{
			var x = vector2Int.x;
			var y = vector2Int.y;
			var linearSteps = Math.Abs(Math.Abs(y) - Math.Abs(x));
			var diagonalSteps = Math.Max(Math.Abs(y), Math.Abs(x)) - linearSteps;
			var sqr = Math.Sqrt(2);
			return linearSteps + sqr * diagonalSteps;
		}
	}
}