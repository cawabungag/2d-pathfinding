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
				{new WindowData() {presenterId = "start_game", viewPath = "Assets/Resources/UI/ViewStart"}};
			var aasas = JsonUtility.ToJson(asd);
			Debug.LogError($"{aasas}");
		}
	}
}