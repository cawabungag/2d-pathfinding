using System;
using Assets.Instantiator;
using Core.Boot;
using Core.Services;
using Core.WindowService;
using States;
using StaticData;
using StaticData.Data;
using UI.Presenters;
using UI.Views;
using Utils;

namespace Factories.UI
{
	public class PresenterFactory : IPresenterFactory
	{
		private readonly IInstantiator _instantiator;
		private readonly CanvasRoot _canvasRoot;
		private readonly StartState _startState;
		private readonly GameState _gameState;

		public PresenterFactory(IInstantiator instantiator, CanvasRoot canvasRoot, StartState startState, GameState gameState)
		{
			_instantiator = instantiator;
			_canvasRoot = canvasRoot;
			_startState = startState;
			_gameState = gameState;
		}
		
		public IPresenter Create(WindowStaticData windowStaticData)
		{
			var viewPath = windowStaticData.viewPath;
			var presenterId = windowStaticData.presenterId;
			var view = _instantiator.InstantiateGameObject(viewPath, _canvasRoot.Root.transform);

			switch (presenterId)
			{
				case PresenterIds.START_GAME:
				{
					var startView = view.GetComponent<StartView>();
					return new StartPresenter(startView, _startState);
				}
				
				case PresenterIds.CIRCLE:
				{
					var circleView = view.GetComponent<CircleView>();
					return new CirclePresenter(circleView);
				}
				
				case PresenterIds.PATH:
				{
					var pathView = view.GetComponent<PathView>();
					return new PathPresenter(pathView);
				}
				
				case PresenterIds.ADD_BUG:
				{
					var addBugView = view.GetComponent<AddBugView>();
					return new AddBugPresenter(addBugView, _gameState);
				}
				
				case PresenterIds.RADIUS:
				{
					var staticData = ServiceLocator.Container.Single<IStaticDataService>();
					var radiusObstacleView = view.GetComponent<RadiusObstacleView>();
					return new RadiusObstaclePresenter(radiusObstacleView, _gameState, staticData);
				}
			}

			throw new InvalidOperationException();
		}
	}
}