using Core;

namespace StaticData
{
	public interface IStaticDataService : IService
	{
		void Initialize();
		WindowStaticData GetWindowData(string presenterId);
		GameRulesStaticData GetGameRulesData();
		BugStaticData GetBugStaticData();
		string GetTilePath();
	}
}