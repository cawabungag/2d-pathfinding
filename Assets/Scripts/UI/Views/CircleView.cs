using Core.WindowService;
using UnityEngine;

namespace UI.Views
{
	public class CircleView : BaseView
	{
		[SerializeField] 
		private LineRenderer _lineRenderer;
		
		public LineRenderer LineRenderer => _lineRenderer;
	}
}