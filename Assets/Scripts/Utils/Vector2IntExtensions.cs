using System;
using UnityEngine;

namespace Utils
{
	public static class Vector2IntExtensions
	{
		public const int DEFAULT_Z_AXIS = -5;

		public static double DistanceEstimate(this Vector2Int vector2Int)
		{
			var x = vector2Int.x;
			var y = vector2Int.y;
			var linearSteps = Math.Abs(Math.Abs(y) - Math.Abs(x));
			var diagonalSteps = Math.Max(Math.Abs(y), Math.Abs(x)) - linearSteps;
			var sqr = Math.Sqrt(2);
			return linearSteps + sqr * diagonalSteps;
		}

		public static Vector3 ToVector3(this Vector2Int vector2Int, int zAxis = DEFAULT_Z_AXIS) => new(vector2Int.x, vector2Int.y, zAxis);
		public static Vector2 ToVector2F(this Vector2Int vector2Int) => new(vector2Int.x, vector2Int.y);
	}
}