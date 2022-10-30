using System;
using UnityEngine;

namespace StaticData
{
	[Serializable]
	public class GameRulesStaticData
	{
		public Vector2Int startPosition;
		public Vector2Int finishPosition;
		
		public int gridWidth;
		public int gridHeight;
	}
}