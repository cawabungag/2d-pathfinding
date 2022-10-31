using Bug;
using Core.Factory;
using Core.Services;
using StaticData.Data.Bug;

namespace Factories.Bug
{
	public interface IBugFactory : IFactory<BugStaticData, IBugPresenter>, IService { }
}