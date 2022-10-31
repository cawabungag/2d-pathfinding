using UnityEngine;

namespace Bug
{
	public interface IBugPresenter
	{
		int CurrentWayPoint { get; }
		Vector2 Position { get; }
		Vector2Int[] Route { get; }
		
		void Move(Vector3 target, float speed, float deltaTime);
		void SetCurrentWayPoint(int newCurrentWayPoint);
		void SetCurrentRoute(Vector2Int[] newRoute);
	}
}