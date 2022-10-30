using Core.WindowService;
using States;
using UI.Views;
using Utils;

namespace UI.StartGame
{
	public class StartPresenter : BasePresenter<StartView>
	{
		private readonly StartState _startState;
		public override string PresenterId => PresenterIds.START_GAME;

		public StartPresenter(StartView view, StartState startState) : base(view)
		{
			_startState = startState;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			View.Button.onClick.AddListener(OnStartClick);
		}

		private void OnStartClick()
		{
			_startState.GoToGameState();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			View.Button.onClick.RemoveListener(OnStartClick);
		}
	}
}