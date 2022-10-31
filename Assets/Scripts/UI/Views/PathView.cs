using Core.WindowService;
using UnityEngine;

namespace UI.Views
{
	public class PathView : BaseView
	{
		[SerializeField] 
		private LineRenderer _lineRenderer;
		
		public LineRenderer LineRenderer => _lineRenderer;
	}
}