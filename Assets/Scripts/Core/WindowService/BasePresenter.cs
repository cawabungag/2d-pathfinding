namespace Core.WindowService
{
	public abstract class BasePresenter<TView> : IPresenter where TView : IView
	{
		public abstract string PresenterId { get; }
		protected TView View { get; }

		protected BasePresenter(TView view)
		{
			View = view;
		}

		public void Open()
		{
			View.Open();
			OnOpen();
		}

		public void Close()
		{
			View.Close();
			OnClose();
		}

		protected virtual void OnOpen(){}
		protected virtual void OnClose(){}
	}
}