using Assets.Instantiator;
using Assets.ResourceLoader;
using Core.SceneManagement;
using Core.Services;
using Core.States;
using Core.WindowService;
using StaticData;
using Utils;

namespace States
{
	public class BoostrapState : BaseState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoaderService _sceneLoaderService;
		private readonly ServiceLocator _services;

		public BoostrapState(GameStateMachine gameStateMachine, SceneLoaderService sceneLoaderService,
			ServiceLocator services)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoaderService = sceneLoaderService;
			_services = services;
		}
	
		public override void Enter()
		{
			RegisterServices();
			_sceneLoaderService.Load(SceneUtils.START_SCENE_NAME, OnStartSceneLoaded);
		}

		private void RegisterServices()
		{
			_services.RegisterSingle<IWindowService>(new WindowService());
			_services.RegisterSingle<IResourceLoader>(new ResourceLoader());
		}

		private void OnStartSceneLoaded() => _gameStateMachine.Enter<StartState>();
	}
}