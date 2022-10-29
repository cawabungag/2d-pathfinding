using Infrastructure;
using Infrastructure.States;

public class BoostrapState : IState
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
	
	public void Enter()
	{
		_sceneLoader.Load(SceneUtils.START_SCENE_NAME, OnEnterStartLevel);
	}

	private void OnEnterStartLevel()
	{
	}

	public void Update()
	{
	}

	public void Exit()
	{
	}
}