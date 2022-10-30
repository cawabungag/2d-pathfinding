using Assets;
using Core.WindowService;
using Infrastructure;
using Infrastructure.States;
using States;
using StaticData;

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

		var resourceLoader = _services.Single<IResourceLoader>();
		IStaticDataService staticDataService = new StaticDataServiceService(resourceLoader);
		staticDataService.Initialize();

		_services.RegisterSingle(staticDataService);
		_services.RegisterSingle<IInstantiator>(new Instantiator(resourceLoader));
	}

	private void OnStartSceneLoaded() => _gameStateMachine.Enter<StartState>();
}