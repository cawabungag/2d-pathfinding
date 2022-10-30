using System;
using Assets;
using Core;
using Core.WindowService;
using States;
using StaticData;
using UI.StartGame;
using UI.Views;
using Utils;

namespace Factories
{
	public class PresenterFactory : IPresenterFactory
	{
		private readonly IInstantiator _instantiator;
		private readonly CanvasRoot _canvasRoot;
		private readonly StartState _startState;

		public PresenterFactory(IInstantiator instantiator, CanvasRoot canvasRoot, StartState startState)
		{
			_instantiator = instantiator;
			_canvasRoot = canvasRoot;
			_startState = startState;
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
			}

			throw new InvalidOperationException();
		}
	}
}