namespace Core.WindowService
{
	public interface IPresenter
	{
		string PresenterId { get; }
		void Open();
		void Close();
		void Dispose();
	}
}