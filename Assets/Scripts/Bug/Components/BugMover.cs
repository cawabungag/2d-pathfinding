using System;
using StaticData.Data.Bug;
using UnityEngine;
using Utils;

namespace Bug.Components
{
	public class BugMover : MonoBehaviour
	{
		private BugState _currentBugState;
		private BugStatsStaticData[] _bugStaticDatas;
		private float _speed;

		public void Move(Vector3 target, float deltaTime)
		{
			var currentBugData = GetCurrentBugData();
			if (_speed < currentBugData.maxSpeed) 
				_speed += currentBugData.acceleration * Time.deltaTime;

			var oldPosition = transform.position;
			var targetPosition = new Vector3(target.x, target.y, VectorsExtensions.DEFAULT_Z_AXIS);
			transform.position = Vector3.MoveTowards(oldPosition, targetPosition, _speed * deltaTime);
		}

		public void SetState(BugState bugState)
			=> _currentBugState = bugState;

		public void SetBugData(BugStatsStaticData[] bugData)
			=> _bugStaticDatas = bugData;

		private BugStatsStaticData GetCurrentBugData()
		{
			foreach (var bugStaticData in _bugStaticDatas)
			{
				if (bugStaticData.bugState == _currentBugState)
				{
					return bugStaticData;
				}
			}

			throw new InvalidOperationException();
		}

		public void SetModifyStatData(BugState bugState, BugStatsStaticData modifyStatdata)
		{
			for (var i = 0; i < _bugStaticDatas.Length; i++)
			{
				var bugStaticData = _bugStaticDatas[i];
				if (bugStaticData.bugState == bugState)
				{
					_bugStaticDatas[i] = modifyStatdata;
				}
			}
		}
	}
}