namespace Infrastructure.States
{
	public abstract class BaseState : IState
	{
		public virtual void Update() { }
		public virtual void Exit() { }
		public virtual void Enter() { }
	}
}