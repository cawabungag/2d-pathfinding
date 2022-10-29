using Infrastructure;
using UnityEngine;

namespace Core
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		private Game _game;

		private void Awake()
		{
			_game = new Game(this);
			_game.StateMachine.Enter<BoostrapState>();
			DontDestroyOnLoad(this);
		}

		private void Update()
		{
			_game.StateMachine.Update();
		}
	}
}