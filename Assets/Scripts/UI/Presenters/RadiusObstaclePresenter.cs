using Core.WindowService;
using States;
using StaticData;
using UI.Views;
using UnityEngine.UI;
using Utils;

namespace UI.Presenters
{
	public class RadiusObstaclePresenter : BasePresenter<RadiusObstacleView>
	{
		private readonly GameState _gameState;
		private readonly IStaticDataService _staticDataService;
		public override string PresenterId => PresenterIds.RADIUS;
		public override bool IsPopUp => true;
		
		public RadiusObstaclePresenter(RadiusObstacleView view, GameState gameState, 
			IStaticDataService staticDataService) : base(view)
		{
			_gameState = gameState;
			_staticDataService = staticDataService;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
			var gameRulesData = _staticDataService.GetGameRulesData();
			var slider = View.Slider;
			slider.direction = Slider.Direction.LeftToRight;
			slider.minValue = gameRulesData.minObstacleRadius;
			slider.maxValue = gameRulesData.maxObstacleRadius;
			slider.wholeNumbers = true;
			slider.value = gameRulesData.obstacleRadius;
			slider.onValueChanged.AddListener(OnRadiusChanged);
		}

		private void OnRadiusChanged(float radius)
		{
			_gameState.SetObstacleRadius(radius);
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			View.Slider.onValueChanged.RemoveListener(OnRadiusChanged);
		}
	}
}