namespace Core.WindowService
{
	public interface IWindowService : IService
	{
		void RegisterPresenter(IPresenter presenter);
		void DisposePresenter(IPresenter presenter);
		
		void Open(string presenterId);
		void Close(string presenterId);
	}
}