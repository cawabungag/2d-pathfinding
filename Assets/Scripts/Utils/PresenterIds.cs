using StaticData;
using UnityEditor;
using UnityEngine;

namespace Utils
{
	public static class PresenterIds
	{
		public const string START_GAME = "start_game";
		
		[MenuItem("/Utils/asdasdasd")]
		public static void aasdasd()
		{
			var asd = new StaticData.StaticData();
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