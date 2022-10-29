namespace Infrastructure.States
{
	public interface IExitableState
	{
		void Update();
		void Exit();
	}
}