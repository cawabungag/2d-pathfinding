using System;
using Assets;
using UnityEngine;

namespace StaticData
{
	public class StaticDataServiceService : IStaticDataService
	{
		private const string STATIC_DATA_PATH = "staticData";
		private readonly IResourceLoader _resourceLoader;
		private StaticData _staticData;

		public StaticDataServiceService(IResourceLoader resourceLoader)
		{
			_resourceLoader = resourceLoader;
		}

		public void Initialize()
		{
			var staticDataTextAsset = _resourceLoader.LoadTextAsset(STATIC_DATA_PATH);
			var text = staticDataTextAsset.text;
			_staticData = JsonUtility.FromJson<StaticData>(text);
		}

		public WindowData GetWindowData(string presenterId)
		{
			foreach (var windowData in _staticData.WindowsData)
			{
				if (windowData.presenterId == presenterId)
				{
					return windowData;
				}
			}

			throw new InvalidOperationException();
		}
	}
	
	
}