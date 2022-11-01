using System;
using Utils;

namespace StaticData.Data.Bug
{
	[Serializable]
	public class BugStatsStaticData
	{
		public BugState bugState;
		public float maxSpeed;
		public float acceleration;
		public float maxAcceleration;

		public BugStatsStaticData(BugState bugState, float maxSpeed, float acceleration, float maxAcceleration)
		{
			this.bugState = bugState;
			this.maxSpeed = maxSpeed;
			this.acceleration = acceleration;
			this.maxAcceleration = maxAcceleration;
		}
		
		public BugStatsStaticData Clone() 
			=> new(bugState, maxSpeed, acceleration, maxAcceleration);
	}
}