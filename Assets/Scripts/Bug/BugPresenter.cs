using Bug.Components;
using UnityEngine;

namespace Bug
{
	public class BugPresenter : IBugPresenter
	{
		private readonly BugView _bugView;

		public BugPresenter(BugView bugView) 
			=> _bugView = bugView;

		public int CurrentWayPoint { get; private set; }

		public Vector2Int[] Route { get; private set; }
		public Vector2 Position => _bugView.transform.position;

		public void Move(Vector3 target, float speed, float deltaTime)
		{
			_bugView.Mover.Move(target, speed, deltaTime);
			var direction = target.normalized;
			_bugView.Rotator.Rotate(direction);
		}

		public void SetCurrentWayPoint(int currentWayPoint) => CurrentWayPoint = currentWayPoint;
		public void SetCurrentRoute(Vector2Int[] newRoute) => Route = newRoute;
	}
}