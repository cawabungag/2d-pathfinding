using Core.WindowService;
using States;
using UI.Views;
using Utils;

namespace UI.Presenters
{
	public class AddBugPresenter : BasePresenter<AddBugView>
	{
		private readonly GameState _gameState;
		public override string PresenterId => PresenterIds.ADD_BUG;
		public override bool IsPopUp => true;

		public AddBugPresenter(AddBugView view, GameState gameState) : base(view)
		{
			_gameState = gameState;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			View.Button.onClick.AddListener(OnAddBugClick);
		}

		private void OnAddBugClick()
		{
			_gameState.AddBug();
		}

		protected override void OnClose()
		{
			base.OnClose();
			View.Button.onClick.RemoveListener(OnAddBugClick);
		}
	}
}