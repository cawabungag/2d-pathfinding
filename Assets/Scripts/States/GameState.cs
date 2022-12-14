using System.Collections.Generic;
using Assets.Instantiator;
using Assets.ResourceLoader;
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
		private GameCheckObstacleService _gameCheckObstacleService;
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

			var addBugWindowData = _staticDataService.GetWindowData(PresenterIds.ADD_BUG);
			var addBugPresenter = presenterFactory.Create(addBugWindowData);
			windowsService.RegisterPresenter(addBugPresenter);

			var radiusWindowData = _staticDataService.GetWindowData(PresenterIds.RADIUS);
			var radiusPresenter = presenterFactory.Create(radiusWindowData);
			windowsService.RegisterPresenter(radiusPresenter);

			var accelerationWindowData = _staticDataService.GetWindowData(PresenterIds.BUG_ACCELERATION);
			var accelerationPresenter = presenterFactory.Create(accelerationWindowData);
			windowsService.RegisterPresenter(accelerationPresenter);

			windowsService.Open(PresenterIds.PATH);
			windowsService.Open(PresenterIds.CIRCLE);
			windowsService.Open(PresenterIds.ADD_BUG);
			windowsService.Open(PresenterIds.RADIUS);
			windowsService.Open(PresenterIds.BUG_ACCELERATION);
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
				(PathPresenter) _pathWindowPresenter);
			_serviceLocator.RegisterSingle(_gameCalculatePathService);

			_gameCheckObstacleService = new GameCheckObstacleService();
			_serviceLocator.RegisterSingle(_gameCheckObstacleService);

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

		public void AddBug()
		{
			var bugFactory = _serviceLocator.Single<IBugFactory>();
			var bugStaticData = _staticDataService.GetBugStaticData();
			var bug = bugFactory.Create(bugStaticData);
			bug.SetBugData(bugStaticData.bugStats);
			_bugsPresenterBuffer.Add(bug);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);

			if (_isExitPending)
				return;

			var obstacleData = _gameObstaclesService.Execute();
			_gameCheckObstacleService.Execute(obstacleData.ObstaclePosition, obstacleData.Radius, _bugsPresenterBuffer);

			if (obstacleData.IsObstacleChanged)
				_gameCalculatePathService.Execute(obstacleData.ObstaclesPointBuffer, _bugsPresenterBuffer);

			_gameMoverService.Execute(_bugsPresenterBuffer, deltaTime);
			_gameCheckFinishService.Execute(_bugsPresenterBuffer);
		}

		public void SetObstacleRadius(float radius)
			=> _gameObstaclesService.SetRadius(radius);


		public void SetWalkAcceleration(float value)
		{
			var bugStaticData = _staticDataService.GetBugStaticData();
			var bugStats = bugStaticData.bugStats;
			foreach (var bugStat in bugStats)
			{
				if (bugStat.bugState == BugState.Walk)
				{
					bugStat.acceleration = value;
					foreach (var bugPresenter in _bugsPresenterBuffer)
					{
						bugPresenter.SetModifyStatData(BugState.Walk, bugStat);
					}
				}
			}
		}

		public void SetAvoidAcceleration(float value)
		{
			var bugStaticData = _staticDataService.GetBugStaticData();
			var bugStats = bugStaticData.bugStats;
			foreach (var bugStat in bugStats)
			{
				if (bugStat.bugState == BugState.Avoid)
				{
					bugStat.acceleration = value;
					foreach (var bugPresenter in _bugsPresenterBuffer)
					{
						bugPresenter.SetModifyStatData(BugState.Avoid, bugStat);
					}
				}
			}
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