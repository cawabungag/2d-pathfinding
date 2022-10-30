using Core;
using Core.Factory;
using Factories.Bug;
using StaticData;

namespace Factories
{
	public interface IBugFactory : IFactory<BugStaticData, IBugPresenter>, IService { }
}