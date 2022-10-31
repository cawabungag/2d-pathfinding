using Core;
using Core.Services;
using StaticData.Data;
using StaticData.Data.Bug;

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