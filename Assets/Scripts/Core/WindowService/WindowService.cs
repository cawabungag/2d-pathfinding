using System;
using System.Collections.Generic;

namespace Core.WindowService
{
	public class WindowService : IWindowService
	{
		private readonly List<IPresenter> _registeredPresenters = new();
		private readonly Stack<IPresenter> _presentersStack = new();
		
		public void RegisterPresenter(IPresenter presenter)
		{
			presenter.Close();
			_registeredPresenters.Add(presenter);
		}

		public void DisposePresenter(IPresenter presenter)
		{
		}

		public void Open(string presenterId)
		{
			var openedPresenter = _presentersStack.Pop();
			openedPresenter.Close();
			
			var presenter = GetPresenter(presenterId);
			_presentersStack.Push(presenter);
			presenter.Open();
		}

		public void Close(string presenterId)
		{
			var openedPresenter = _presentersStack.Pop();
			openedPresenter.Close();
		}

		private IPresenter GetPresenter(string presenterId)
		{
			foreach (var presenter in _registeredPresenters)
				if (presenter.PresenterId == presenterId)
					return presenter;

			throw new InvalidOperationException($"There is no presenter with the type of {presenterId}");
		}
	}
}