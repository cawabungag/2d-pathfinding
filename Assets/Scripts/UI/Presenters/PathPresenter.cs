using Core.WindowService;
using UI.Views;
using UnityEngine;
using Utils;

namespace UI.Presenters
{
	public class PathPresenter : BasePresenter<PathView>, IPopUp
	{
		private const float LINE_WIDTH = 0.1f;
		public override string PresenterId => PresenterIds.PATH;
		public override bool IsPopUp => true;

		public PathPresenter(PathView view) : base(view)
		{
		}

		public void DrawPath(Vector2Int[] path)
		{
			var lineRenderer = View.LineRenderer;
			lineRenderer.startWidth = LINE_WIDTH;
			lineRenderer.endWidth = LINE_WIDTH;
			lineRenderer.positionCount = path.Length;
			
			for (var index = 0; index < path.Length; index++)
			{
				var pathPoint = path[index];
				lineRenderer.SetPosition(index, pathPoint.ToVector3());
			}
		}
	}
}