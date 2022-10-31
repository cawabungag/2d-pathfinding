namespace Core.States
{
	public interface IPayloadedState<TPayload> : IExitableState
	{
		void Enter(TPayload payload);
	}
}