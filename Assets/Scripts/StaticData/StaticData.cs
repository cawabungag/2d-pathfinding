using System;

namespace StaticData
{
	[Serializable]
	public class StaticData
	{
		public WindowData[] windowsData;
		
		public WindowData[] WindowsData
		{
			get => windowsData;
			set => windowsData = value;
		}
	}
}