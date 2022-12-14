using System;
using Assets.ResourceLoader;
using StaticData.Data;
using StaticData.Data.Bug;
using UnityEngine;

namespace StaticData
{
	public class StaticDataServiceService : IStaticDataService
	{
		private const string STATIC_DATA_PATH = "staticData";
		private readonly IResourceLoader _resourceLoader;
		private Data.StaticData _staticData;

		public StaticDataServiceService(IResourceLoader resourceLoader)
		{
			_resourceLoader = resourceLoader;
		}

		public void Initialize()
		{
			var staticDataTextAsset = _resourceLoader.LoadTextAsset(STATIC_DATA_PATH);
			var text = staticDataTextAsset.text;
			_staticData = JsonUtility.FromJson<Data.StaticData>(text);
		}

		public WindowStaticData GetWindowData(string presenterId)
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

		public GameRulesStaticData GetGameRulesData() => _staticData.gameRulesStaticData;
		public BugStaticData GetBugStaticData() => _staticData.bugStaticData;
		public string GetTilePath() => _staticData.tilePath;
	}
}