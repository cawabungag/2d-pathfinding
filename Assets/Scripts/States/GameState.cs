using System.Collections.Generic;
using System.Linq;
using Assets.Instantiator;
using Bug;
using Core.SceneManagement;
using Core.Services;
using Core.States;
using Core.WindowService;
using Factories.Bug;
using Factories.Tile;
using Factories.UI;
using Game;
using Grid;
using InputService;
using Pathfinding;
using StaticData;
using UI.Presenters;
using Utils;

namespace States
{
	public class GameState : BaseState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoaderService _sceneLoaderService;
		private readonly ServiceLocator _serviceLocator;
		private IStaticDataService _staticDataService;

		private readonly List<IBugPresenter> _bugsPresenterBuffer = new();

		private bool _isExitPending;
		
		private GameCalculatePathService _gameCalculatePathService;
		private GameObstaclesService _gameObstaclesService;
		private GameMoverService _gameMoverService;
		private GameCheckFinishService _gameCheckFinishService;
		private IPresenter _circleWindowPresenter;
		private IPresenter _pathWindowPresenter;

		public GameState(GameStateMachine gameStateMachine, ServiceLocator serviceLocator,
			SceneLoaderService sceneLoaderService)
		{
			_gameStateMachine = gameStateMachine;
			_serviceLocator = serviceLocator;
			_sceneLoaderService = sceneLoaderService;
		}

		public override void Enter()
		{
			base.Enter();
			_staticDataService = _serviceLocator.Single<IStaticDataService>();
			RegisterPresenters();
			RegisterServices();
			Initialize();
		}
		
		private void RegisterPresenters()
		{
			var presenterFactory = _serviceLocator.Single<IPresenterFactory>();
			var windowsService = _serviceLocator.Single<IWindowService>();
			
			var circleWindowData = _staticDataService.GetWindowData(PresenterIds.CIRCLE);
			_circleWindowPresenter = presenterFactory.Create(circleWindowData);
			windowsService.RegisterPresenter(_circleWindowPresenter);
			
			var pathWindowData = _staticDataService.GetWindowData(PresenterIds.PATH);
			_pathWindowPresenter = presenterFactory.Create(pathWindowData);
			windowsService.RegisterPresenter(_pathWindowPresenter);
			
			windowsService.Open(PresenterIds.PATH);
			windowsService.Open(PresenterIds.CIRCLE);
		}

		private void RegisterServices()
		{
			var pathfindingService = new PathfindingService();
			var instantiator = _serviceLocator.Single<IInstantiator>();

			_serviceLocator.RegisterSingle<IPathfindingService>(pathfindingService);
			_serviceLocator.RegisterSingle<IBugFactory>(new BugFactory(instantiator, _staticDataService));

			var tileFactory = new TileFactory(instantiator, _staticDataService);
			_serviceLocator.RegisterSingle<ITileFactory>(tileFactory);

			_gameCheckFinishService = new GameCheckFinishService(_staticDataService, this);
			_serviceLocator.RegisterSingle(_gameCheckFinishService);

			var inputService = new InputService.InputService();
			_serviceLocator.RegisterSingle<IInputService>(inputService);

			_gameMoverService = new GameMoverService();
			_serviceLocator.RegisterSingle(_gameMoverService);

			_gameObstaclesService = new GameObstaclesService(inputService, _staticDataService, 
				(CirclePresenter) _circleWindowPresenter);
			_serviceLocator.RegisterSingle(_gameObstaclesService);

			_gameCalculatePathService = new GameCalculatePathService(pathfindingService, _staticDataService, 
				(PathPresenter)_pathWindowPresenter);
			_serviceLocator.RegisterSingle(_gameCalculatePathService);

			var gridService = new GridService(tileFactory);
			_serviceLocator.RegisterSingle<IGridService>(gridService);
		}

		private void Initialize()
		{
			var gridService = _serviceLocator.Single<IGridService>();
			var gameRules = _staticDataService.GetGameRulesData();

			var startPosition = gameRules.startPosition;
			gridService.CreateTile(startPosition);
			var startTile = gridService.GetTile(startPosition);
			startTile.SetStart();

			var finishPosition = gameRules.finishPosition;
			gridService.CreateTile(finishPosition);
			var finishTile = gridService.GetTile(finishPosition);
			finishTile.SetFinish();
			
			AddBug();
			_isExitPending = false;
		}

		private void AddBug()
		{
			var bugFactory = _serviceLocator.Single<IBugFactory>();
			var bugStaticData = _staticDataService.GetBugStaticData();
			var bug = bugFactory.Create(bugStaticData);
			_bugsPresenterBuffer.Add(bug);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			if (_isExitPending)
				return;

			var (isObstacleChanged, obstacles) = _gameObstaclesService.Execute();
			
			if (isObstacleChanged) 
				_gameCalculatePathService.Execute(obstacles, _bugsPresenterBuffer);
			
			_gameMoverService.Execute(_bugsPresenterBuffer, deltaTime);
			_gameCheckFinishService.Execute(_bugsPresenterBuffer);
		}

		public override void Exit()
		{
			base.Exit();
			_bugsPresenterBuffer.Clear();
			var windowService = _serviceLocator.Single<IWindowService>();
			windowService.DisposePresenters();
		}

		public void ExitGame()
		{
			_isExitPending = true;
			_sceneLoaderService.Load(SceneUtils.START_SCENE_NAME, OnStartSceneLoaded);
		}

		private void OnStartSceneLoaded() => _gameStateMachine.Enter<StartState>();
	}
}