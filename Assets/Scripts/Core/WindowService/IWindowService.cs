using Core.Services;

namespace Core.WindowService
{
	public interface IWindowService : IService
	{
		void RegisterPresenter(IPresenter presenter);
		void DisposePresenters();
		
		void Open(string presenterId);
		void Close(string presenterId);
	}
}