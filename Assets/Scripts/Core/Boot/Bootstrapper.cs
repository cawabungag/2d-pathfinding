using Core.SceneManagement;
using Core.Services;
using Core.States;
using States;
using UnityEngine;

namespace Core.Boot
{
	public class Bootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] 
		private CanvasRoot _canvasRoot;
		
		private GameStateMachine _gameStateMachine;

		private void Awake()
		{
			var sceneLoader = new SceneLoaderService(this);
			var container = ServiceLocator.Container;
			
			_gameStateMachine = new GameStateMachine(sceneLoader, container, _canvasRoot);
			_gameStateMachine.Enter<BoostrapState>();
			DontDestroyOnLoad(this);
		}

		private void Update() => _gameStateMachine.Update(Time.deltaTime);
	}
}