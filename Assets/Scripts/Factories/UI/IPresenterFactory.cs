using Core.Factory;
using Core.Services;
using Core.WindowService;
using StaticData.Data;

namespace Factories.UI
{
	public interface IPresenterFactory : IFactory<WindowStaticData, IPresenter>, IService { }
}