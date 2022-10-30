using System.Collections.Generic;
using System.Linq;
using Assets;
using Factories;
using Factories.Bug;
using Factories.Tile;
using Game;
using Grid;
using Infrastructure;
using Infrastructure.States;
using InputService;
using Pathfinding;
using StaticData;

namespace States
{
	public class GameState : BaseState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoaderService _sceneLoaderService;
		private readonly ServiceLocator _serviceLocator;
		private IStaticDataService _staticDataService;
		
		private readonly Dictionary<int, IBugPresenter> _bugsPresenterBuffer = new();

		private bool _isExitPending;

		public GameState(GameStateMachine gameStateMachine, ServiceLocator serviceLocator, SceneLoaderService sceneLoaderService)
		{
			_gameStateMachine = gameStateMachine;
			_serviceLocator = serviceLocator;
			_sceneLoaderService = sceneLoaderService;
		}

		public override void Enter()
		{
			base.Enter();
			RegisterServices();
			Initialize();
		}

		public override void Exit()
		{
			base.Exit();
			_bugsPresenterBuffer.Clear();
		}

		private void Initialize()
		{
			var gridService = _serviceLocator.Single<IGridService>();
			var gameRules = _staticDataService.GetGameRulesData();
			var gridWidth = gameRules.gridWidth;
			var gridHeight = gameRules.gridHeight;
			gridService.GenerateGrid(gridWidth, gridHeight);
			
			var startPosition = gameRules.startPosition;
			var startTile = gridService.GetTile(startPosition);
			startTile.SetStart();

			var finishPosition = gameRules.finishPosition;
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
			var hash = "axelbolt".ToCharArray().Sum(x => x) % 100;
			
			_bugsPresenterBuffer.Add(hash, bug);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
			
			if (_isExitPending)
				return;
			
			var checkFinishService = _serviceLocator.Single<GameCheckFinishService>();
			var moverService = _serviceLocator.Single<GameMoverService>();
			var obstaclesService = _serviceLocator.Single<GameObstaclesService>();
			var pathFindService = _serviceLocator.Single<GameCalculatePathService>();
			var gridService = _serviceLocator.Single<IGridService>();
			
			gridService.Clear();
			var obstacles = obstaclesService.Execute();
			var bugsRoutesBuffer = pathFindService.Execute(obstacles, _bugsPresenterBuffer);
			checkFinishService.Execute(_bugsPresenterBuffer.Values);
			moverService.Execute(_bugsPresenterBuffer, bugsRoutesBuffer, deltaTime);
		}

		private void RegisterServices()
		{
			_staticDataService = _serviceLocator.Single<IStaticDataService>();
			var pathfindingService = new PathfindingService();
			var instantiator = _serviceLocator.Single<IInstantiator>();

			_serviceLocator.RegisterSingle<IPathfindingService>(pathfindingService);
			_serviceLocator.RegisterSingle<IBugFactory>(new BugFactory(instantiator, _staticDataService));

			var tileFactory = new TileFactory(instantiator, _staticDataService);
			_serviceLocator.RegisterSingle<ITileFactory>(tileFactory);
			
			var gridService = new GridService(tileFactory);
			_serviceLocator.RegisterSingle<IGridService>(gridService);

			var gameCheckFinishService = new GameCheckFinishService(_staticDataService, this);
			_serviceLocator.RegisterSingle(gameCheckFinishService);

			var inputService = new InputService.InputService();
			_serviceLocator.RegisterSingle<IInputService>(inputService);

			var gameMoverService = new GameMoverService();
			_serviceLocator.RegisterSingle(gameMoverService);

			var gameObstacleService = new GameObstaclesService(inputService, gridService, _staticDataService);
			_serviceLocator.RegisterSingle(gameObstacleService);

			var gameCalculatePathService = new GameCalculatePathService(pathfindingService, _staticDataService, gridService);
			_serviceLocator.RegisterSingle(gameCalculatePathService);
		}

		public void ExitGame()
		{
			_isExitPending = true;
			_sceneLoaderService.Load(SceneUtils.START_SCENE_NAME, OnStartSceneLoaded);
		}

		private void OnStartSceneLoaded() => _gameStateMachine.Enter<StartState>();
	}
}