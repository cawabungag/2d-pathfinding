using Assets.Instantiator;
using Bug;
using Bug.Components;
using StaticData;
using StaticData.Data.Bug;
using UnityEngine;

namespace Factories.Bug
{
	public class BugFactory : IBugFactory
	{
		private readonly IInstantiator _instantiator;
		private readonly IStaticDataService _staticDataService;

		public BugFactory(IInstantiator instantiator, IStaticDataService staticDataService)
		{
			_instantiator = instantiator;
			_staticDataService = staticDataService;
		}
		
		public IBugPresenter Create(BugStaticData parameter)
		{
			var viewPath = parameter.viewPath;
			var gameRules = _staticDataService.GetGameRulesData();
			var gameObject = _instantiator.InstantiateGameObject(viewPath, gameRules.startPosition,
				Quaternion.identity);
			var view = gameObject.GetComponent<BugView>();
			var presenter = new BugPresenter(view);
			return presenter;
		}
	}
}