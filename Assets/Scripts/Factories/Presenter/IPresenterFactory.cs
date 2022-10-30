using Core;
using Core.Factory;
using Core.WindowService;
using StaticData;

namespace Factories
{
	public interface IPresenterFactory : IFactory<WindowData, IPresenter>, IService { }
}