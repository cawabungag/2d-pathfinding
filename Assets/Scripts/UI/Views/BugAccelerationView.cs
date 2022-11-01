using Core.WindowService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
	public class BugAccelerationView : BaseView
	{
		[SerializeField] 
		private Slider _avoidAccelerationSlider;
		
		[SerializeField] 
		private Slider _walkAccelerationSlider;
		
		public Slider AvoidAccelerationSlider => _avoidAccelerationSlider;
		public Slider WalkAccelerationSlider => _walkAccelerationSlider;
	}
}