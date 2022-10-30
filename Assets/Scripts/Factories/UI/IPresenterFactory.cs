using Core;
using Core.Factory;
using Core.WindowService;
using StaticData;

namespace Factories
{
	public interface IPresenterFactory : IFactory<WindowStaticData, IPresenter>, IService { }
}