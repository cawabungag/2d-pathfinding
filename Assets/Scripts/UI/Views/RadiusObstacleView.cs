using Core.WindowService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
	public class RadiusObstacleView : BaseView
	{
		[SerializeField] 
		private Slider _slider;
		public Slider Slider => _slider;
	}
}