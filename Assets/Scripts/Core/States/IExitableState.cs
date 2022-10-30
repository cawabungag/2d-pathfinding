namespace Infrastructure.States
{
	public interface IExitableState
	{
		void Update(float deltaTime);
		void Exit();
	}
}