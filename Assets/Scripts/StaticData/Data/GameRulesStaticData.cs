using System;
using UnityEngine;

namespace StaticData.Data
{
	[Serializable]
	public class GameRulesStaticData
	{
		public Vector2Int startPosition;
		public Vector2Int finishPosition;
		
		public int gridWidth;
		public int gridHeight;

		public int minObstacleRadius;
		public int maxObstacleRadius;
		public int obstacleRadius;
	}
}