using System.Collections.Generic;
using Core;
using Factories.Bug;
using UnityEngine;
using Utils;

namespace Game
{
	public class GameMoverService : IService
	{
		private readonly Dictionary<int, int> _bugHashByCurrentWayPointBuffer = new();

		public void Execute(IReadOnlyDictionary<int, IBugPresenter> bugPresenters, 
			IReadOnlyDictionary<int, Vector2Int[]> bugRoutes, float deltaTime)
		{
			foreach (var bugByHash in bugPresenters)
			{
				var bugHash = bugByHash.Key;
				var bugPresenter = bugByHash.Value;

				if (!bugRoutes.TryGetValue(bugHash, out var routes)) 
					continue;
				
				var containCurrentWayPoint =
					_bugHashByCurrentWayPointBuffer.TryGetValue(bugHash, out var currentWayPoint);

				if (currentWayPoint == routes.Length) 
					continue;
					
				var targetWayPoint = routes[currentWayPoint];
				var targetPosition = targetWayPoint.ToVector3();
				bugPresenter.Move(targetPosition, 10, deltaTime);

				if (bugPresenter.Position != targetWayPoint) 
					continue;
						
				currentWayPoint++;
				if (containCurrentWayPoint)
				{
					_bugHashByCurrentWayPointBuffer[bugHash] = currentWayPoint;
					continue;
				}
						
				_bugHashByCurrentWayPointBuffer.Add(bugHash, currentWayPoint);
			}
		}
	}
}