namespace Infrastructure.States
{
	public abstract class BaseState : IState
	{
		public virtual void Update(float deltaTime) { }
		public virtual void Exit() { }
		public virtual void Enter() { }
	}
}