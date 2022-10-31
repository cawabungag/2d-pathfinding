using Core.WindowService;
using UI.Views;
using UnityEngine;
using Utils;

namespace UI.Presenters
{
	public class CirclePresenter : BasePresenter<CircleView>
	{
		private const int CIRCLE_SEGMENTS = 360;
		private const float ANGLE_PER_SEGMENT = Mathf.PI / CIRCLE_SEGMENTS * 2;
		private const float LINE_WIDTH = 0.2f;

		public override string PresenterId => PresenterIds.CIRCLE;
		public override bool IsPopUp => true;

		public CirclePresenter(CircleView view) : base(view)
		{
		}

		public void DrawCircle(Vector3 position, int radius)
		{
			var lineRenderer = View.LineRenderer;
			lineRenderer.startWidth = LINE_WIDTH;
			lineRenderer.endWidth = LINE_WIDTH;
			lineRenderer.loop = true;
			lineRenderer.positionCount = CIRCLE_SEGMENTS;
			lineRenderer.numCornerVertices = 100;

			for (int i = 0; i < CIRCLE_SEGMENTS; i++)
			{
				var rotationMatrix = new Matrix4x4(
					new Vector4(Mathf.Cos(ANGLE_PER_SEGMENT * i), Mathf.Sin(ANGLE_PER_SEGMENT * i), 0, 0),
					new Vector4(-1 * Mathf.Sin(ANGLE_PER_SEGMENT * i), Mathf.Cos(ANGLE_PER_SEGMENT * i), 0, 0),
					new Vector4(0, 0, 1, 0),
					new Vector4(0, 0, 0, 1));
				var initialRelativePosition = new Vector3(0, radius, 0);
				lineRenderer.SetPosition(i, position + rotationMatrix.MultiplyPoint(initialRelativePosition));
			}
		}
	}
}