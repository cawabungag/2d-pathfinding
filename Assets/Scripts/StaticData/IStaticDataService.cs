using Core;

namespace StaticData
{
	public interface IStaticDataService : IService
	{
		void Initialize();
		WindowData GetWindowData(string presenterId);
	}
}