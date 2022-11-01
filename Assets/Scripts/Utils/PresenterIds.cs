using StaticData.Data;
using StaticData.Data.Bug;
using UnityEditor;
using UnityEngine;

namespace Utils
{
	public static class PresenterIds
	{
		public const string START_GAME = "start_game";
		public const string CIRCLE = "circle";
		public const string PATH = "path";
		public const string ADD_BUG = "add_bug";
		public const string RADIUS = "radius";
		
		[MenuItem("/Utils/asdasdasd")]
		public static void aasdasd()
		{
			var asd = new StaticData.Data.StaticData();
			asd.WindowsData = new[]
				{new WindowStaticData() {presenterId = "start_game", viewPath = "Assets/Resources/UI/ViewStart"}};
			asd.gameRulesStaticData = new GameRulesStaticData()
			{
				startPosition = new Vector2Int(0, 0),
				finishPosition = new Vector2Int(179, 89),
				gridWidth = 180,
				gridHeight = 90
			};

			asd.bugStaticData = new BugStaticData()
			{
				viewPath = "Prefabs/Bug"
			};
			asd.tilePath = "Prefabs/Tile";
	
			var aasas = JsonUtility.ToJson(asd);
			Debug.LogError($"{aasas}");
		}
	}
}