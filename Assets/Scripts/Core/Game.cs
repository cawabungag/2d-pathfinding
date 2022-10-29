using Core;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ServiceLocator.Container);
        }

        public void Update()
        {
            StateMachine.Update();
        }
    }
}