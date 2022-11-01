using StaticData.Data.Bug;
using UnityEngine;
using Utils;

namespace Bug
{
	public interface IBugPresenter
	{
		int CurrentWayPoint { get; }
		Vector2 Position { get; }
		Vector2Int[] Route { get; }
		BugState CurrentState { get; }

		void Move(Vector3 target, float deltaTime);
		void SetBugData(BugStatsStaticData[] bugData);
		void SetModifyStatData(BugState bugState, BugStatsStaticData walkStatData);
		void SetCurrentWayPoint(int newCurrentWayPoint);
		void SetCurrentRoute(Vector2Int[] newRoute);
		void SetState(BugState bugState);
	}
}