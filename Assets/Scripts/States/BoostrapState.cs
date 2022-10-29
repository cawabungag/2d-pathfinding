using Assets;
using Core.WindowService;
using Infrastructure;
using Infrastructure.States;
using States;

public class BoostrapState : BaseState
{
	private readonly GameStateMachine _gameStateMachine;
	private readonly SceneLoader _sceneLoader;
	private readonly ServiceLocator _services;

	public BoostrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services)
	{
		_gameStateMachine = gameStateMachine;
		_sceneLoader = sceneLoader;
		_services = services;
	}
	
	public override void Enter()
	{
		RegisterServices();
		_sceneLoader.Load(SceneUtils.START_SCENE_NAME, OnEnterStartLevel);
	}

	private void RegisterServices()
	{
		_services.RegisterSingle<IWindowService>(new WindowService());
		_services.RegisterSingle<IResourceLoader>(new ResourceLoader());

		var resourceLoader = _services.Single<IResourceLoader>();
		_services.RegisterSingle<IInstantiator>(new Instantiator(resourceLoader));
	}

	private void OnEnterStartLevel()
	{
		_gameStateMachine.Enter<StartState>();
	}
}