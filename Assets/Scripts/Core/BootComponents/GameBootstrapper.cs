using Infrastructure;
using UnityEngine;

namespace Core
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		private GameStateMachine _gameStateMachine;

		private void Awake()
		{
			var sceneLoader = new SceneLoader(this);
			_gameStateMachine = new GameStateMachine(sceneLoader, ServiceLocator.Container);
			_gameStateMachine.Enter<BoostrapState>();
			DontDestroyOnLoad(this);
		}

		private void Update() => _gameStateMachine.Update();
	}
}