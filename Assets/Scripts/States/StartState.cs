using Assets;
using Assets.Instantiator;
using Core;
using Core.Boot;
using Core.SceneManagement;
using Core.Services;
using Core.States;
using Core.WindowService;
using Factories;
using Factories.UI;
using StaticData;
using Utils;

namespace States
{
	public class StartState : BaseState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly ServiceLocator _services;
		private readonly CanvasRoot _canvasRoot;
		private readonly SceneLoaderService _sceneLoaderService;
		private WindowService _windowsService;
		private PresenterFactory _presenterFactory;

		public StartState(GameStateMachine gameStateMachine, ServiceLocator services, CanvasRoot canvasRoot,
			SceneLoaderService sceneLoaderService)
		{
			_gameStateMachine = gameStateMachine;
			_services = services;
			_canvasRoot = canvasRoot;
			_sceneLoaderService = sceneLoaderService;
		}

		public override void Enter()
		{
			base.Enter();
			RegisterServices();
			RegisterPresenters();
		}

		private void RegisterServices()
		{
			var instantiator = _services.Single<IInstantiator>();
			_presenterFactory = new PresenterFactory(instantiator, _canvasRoot, this);
			_windowsService = new WindowService();
			
			_services.RegisterSingle<IPresenterFactory>(_presenterFactory);
			_services.RegisterSingle<IWindowService>(_windowsService);
		}
		
		private void RegisterPresenters()
		{
			var staticDataService = _services.Single<IStaticDataService>();
			var startGameWindowData = staticDataService.GetWindowData(PresenterIds.START_GAME);
			var startGamePresenter = _presenterFactory.Create(startGameWindowData);
			_windowsService.RegisterPresenter(startGamePresenter);
			_windowsService.Open(PresenterIds.START_GAME);
		}

		public override void Exit()
		{
			base.Exit();
			var windowService = _services.Single<IWindowService>();
			windowService.DisposePresenters();
		}

		public void GoToGameState() => _sceneLoaderService.Load(SceneUtils.GAME_SCENE_NAME, OnGameSceneLoaded);
		private void OnGameSceneLoaded() => _gameStateMachine.Enter<GameState>();
	}
}