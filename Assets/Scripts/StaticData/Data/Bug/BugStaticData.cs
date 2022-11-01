using System;

namespace StaticData.Data.Bug
{
	[Serializable]
	public class BugStaticData
	{
		public string viewPath;
		public BugStatsStaticData[] bugStats;
	}
}