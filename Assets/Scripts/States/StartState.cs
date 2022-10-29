using Infrastructure.States;

namespace States
{
	public class StartState : BaseState
	{
		private readonly GameStateMachine _gameStateMachine;

		public StartState(GameStateMachine gameStateMachine)
		{
			_gameStateMachine = gameStateMachine;
		}
		
		public void Enter()
		{
		}


		public void GoToGame() => _gameStateMachine.Enter<GameState>();
	}
}