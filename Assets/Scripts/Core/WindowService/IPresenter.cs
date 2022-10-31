namespace Core.WindowService
{
	public interface IPresenter
	{
		string PresenterId { get; }
		bool IsPopUp { get; }
		void Open();
		void Close();
		void Dispose();
	}
}