using Bug.Components;
using StaticData;
using StaticData.Data.Bug;
using UnityEngine;
using Utils;

namespace Bug
{
	public class BugPresenter : IBugPresenter
	{
		private readonly BugView _bugView;
		private readonly IStaticDataService _staticDataService;
		
		public int CurrentWayPoint { get; private set; }
		public Vector2Int[] Route { get; private set; }
		public BugState CurrentState { get; private set; }
		public Vector2 Position => _bugView.transform.position;

		public BugPresenter(BugView bugView)
		{
			_bugView = bugView;
		}

		public void SetBugData(BugStatsStaticData[] bugData) 
			=> _bugView.Mover.SetBugData(bugData);

		public void SetModifyStatData(BugState bugState, BugStatsStaticData walkStatData)
			=> _bugView.Mover.SetModifyStatData(bugState, walkStatData);

		public void Move(Vector3 target, float deltaTime)
		{
			_bugView.Mover.Move(target, deltaTime);
			_bugView.Rotator.Rotate(target);
		}

		public void SetCurrentWayPoint(int currentWayPoint) => CurrentWayPoint = currentWayPoint;
		public void SetCurrentRoute(Vector2Int[] newRoute) => Route = newRoute;

		public void SetState(BugState bugState)
		{
			CurrentState = bugState;
			_bugView.Mover.SetState(bugState);
		}
	}
}