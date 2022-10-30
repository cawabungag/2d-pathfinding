using System;

namespace StaticData
{
	[Serializable]
	public class StaticData
	{
		public WindowStaticData[] windowsData;
		public GameRulesStaticData gameRulesStaticData;
		public BugStaticData bugStaticData;
		public string tilePath;

		public WindowStaticData[] WindowsData
		{
			get => windowsData;
			set => windowsData = value;
		}
	}
}