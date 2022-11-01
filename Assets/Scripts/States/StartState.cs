using Assets.Instantiator;
using Assets.ResourceLoader;
using Core.Boot;
using Core.SceneManagement;
using Core.Services;
using Core.States;
using Core.WindowService;
using Factories.UI;
using StaticData;
using Utils;

namespace States
{
	public class StartState : BaseState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly ServiceLocator _serviceLocator;
		private readonly CanvasRoot _canvasRoot;
		private readonly SceneLoaderService _sceneLoaderService;
		private WindowService _windowsService;
		private PresenterFactory _presenterFactory;

		public StartState(GameStateMachine gameStateMachine, ServiceLocator serviceLocator, CanvasRoot canvasRoot,
			SceneLoaderService sceneLoaderService)
		{
			_gameStateMachine = gameStateMachine;
			_serviceLocator = serviceLocator;
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
			var resourceLoader = _serviceLocator.Single<IResourceLoader>();
			var staticDataService = new StaticDataServiceService(resourceLoader);
			staticDataService.Initialize();
			
			_serviceLocator.RegisterSingle<IStaticDataService>(staticDataService);
			_serviceLocator.RegisterSingle<IInstantiator>(new Instantiator(resourceLoader));
			
			var instantiator = _serviceLocator.Single<IInstantiator>();
			_presenterFactory = new PresenterFactory(instantiator, _canvasRoot, this, 
				_gameStateMachine.GetState<GameState>());
			_windowsService = new WindowService();
			
			_serviceLocator.RegisterSingle<IPresenterFactory>(_presenterFactory);
			_serviceLocator.RegisterSingle<IWindowService>(_windowsService);
		}
		
		private void RegisterPresenters()
		{
			var staticDataService = _serviceLocator.Single<IStaticDataService>();
			var startGameWindowData = staticDataService.GetWindowData(PresenterIds.START_GAME);
			var startGamePresenter = _presenterFactory.Create(startGameWindowData);
			_windowsService.RegisterPresenter(startGamePresenter);
			_windowsService.Open(PresenterIds.START_GAME);
		}

		public override void Exit()
		{
			base.Exit();
			var windowService = _serviceLocator.Single<IWindowService>();
			windowService.DisposePresenters();
		}

		public void GoToGameState() => _sceneLoaderService.Load(SceneUtils.GAME_SCENE_NAME, OnGameSceneLoaded);
		private void OnGameSceneLoaded() => _gameStateMachine.Enter<GameState>();
	}
}