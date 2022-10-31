using UnityEngine;

namespace Core.WindowService
{
	public abstract class BasePresenter<TView> : IPresenter where TView : Object, IView
	{
		public abstract string PresenterId { get; }
		public abstract bool IsPopUp { get; }
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

		public void Dispose()
		{
			Object.DestroyImmediate(View);
		}

		protected virtual void OnOpen(){}
		protected virtual void OnClose(){}
	}
}