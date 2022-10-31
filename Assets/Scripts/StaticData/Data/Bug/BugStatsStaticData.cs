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
	}
}