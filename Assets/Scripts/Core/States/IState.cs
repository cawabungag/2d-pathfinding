namespace Core.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}