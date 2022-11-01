using System.Collections.Generic;
using Bug;
using Core.Services;
using UnityEngine;
using Utils;

namespace Game
{
	public class GameCheckObstacleService : IService
	{
		public void Execute(Vector2 obstaclePosition, int obstacleRadius, IReadOnlyList<IBugPresenter> bugs)
		{
			if (bugs.Count == 0)
				return;

			foreach (var bug in bugs)
			{
				var bugPosition = bug.Position;
				if (VectorsExtensions.InCircle(bugPosition, obstaclePosition, obstacleRadius))
				{
					bug.SetState(BugState.Avoid);
					continue;
				}

				bug.SetState(BugState.Walk);
			}
		}
	}
}