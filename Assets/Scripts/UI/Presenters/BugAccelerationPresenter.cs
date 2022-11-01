using Core.Services;
using Core.WindowService;
using States;
using StaticData;
using UI.Views;
using Utils;

namespace UI.Presenters
{
	public class BugAccelerationPresenter : BasePresenter<BugAccelerationView>
	{
		private readonly GameState _gameState;
		private readonly IStaticDataService _staticDataService;
		public override bool IsPopUp => true;
		public override string PresenterId => PresenterIds.BUG_ACCELERATION;

		public BugAccelerationPresenter(BugAccelerationView view, GameState gameState) : base(view)
		{
			_gameState = gameState;
			_staticDataService = ServiceLocator.Container.Single<IStaticDataService>();
		}

		protected override void OnOpen()
		{
			var bugStaticData = _staticDataService.GetBugStaticData();
			var bugStats = bugStaticData.bugStats;
			
			var viewWalkAccelerationSlider = View.WalkAccelerationSlider;
			var viewAvoidAccelerationSlider = View.AvoidAccelerationSlider;
			
			viewWalkAccelerationSlider.onValueChanged.AddListener(OnWalkAccelerationSliderChanged);
			viewAvoidAccelerationSlider.onValueChanged.AddListener(OnAvoidAccelerationSliderChanged);
			
			foreach (var bugStat in bugStats)
			{
				if (bugStat.bugState == BugState.Walk)
				{
					viewWalkAccelerationSlider.maxValue = bugStat.maxAcceleration;
					viewWalkAccelerationSlider.value = bugStat.acceleration;
					continue;
				}
				
				viewAvoidAccelerationSlider.maxValue = bugStat.maxAcceleration;
				viewAvoidAccelerationSlider.value = bugStat.acceleration;
			}
		}

		private void OnAvoidAccelerationSliderChanged(float value) 
			=> _gameState.SetAvoidAcceleration(value);

		private void OnWalkAccelerationSliderChanged(float value) 
			=> _gameState.SetWalkAcceleration(value);

		protected override void OnClose()
		{
			View.WalkAccelerationSlider.onValueChanged.RemoveListener(OnWalkAccelerationSliderChanged);
			View.AvoidAccelerationSlider.onValueChanged.RemoveListener(OnAvoidAccelerationSliderChanged);
		}
	}
}