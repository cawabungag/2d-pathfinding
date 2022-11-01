using System.Collections.Generic;
using Bug;
using Core.Services;
using Utils;

namespace Game
{
	public class GameMoverService : IService
	{
		public void Execute(List<IBugPresenter> bugPresenters, float deltaTime)
		{
			foreach (var bugPresenter in bugPresenters)
			{
				var route = bugPresenter.Route;
				var currentWayPoint = bugPresenter.CurrentWayPoint;
				
				if (currentWayPoint == route.Length) 
					currentWayPoint = 0;

				var targetWayPoint = route[currentWayPoint];
				var targetPosition = targetWayPoint.ToVector3();
				bugPresenter.Move(targetPosition, deltaTime);

				if (bugPresenter.Position != targetWayPoint) 
					continue;
						
				currentWayPoint++;
				bugPresenter.SetCurrentWayPoint(currentWayPoint);
			}
		}
	}
}