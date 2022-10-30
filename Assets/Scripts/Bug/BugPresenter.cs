using UnityEngine;

namespace Factories.Bug
{
	public class BugPresenter : IBugPresenter
	{
		private readonly BugView _bugView;

		public BugPresenter(BugView bugView)
		{
			_bugView = bugView;
		}

		public void Initialize()
		{
			// throw new System.NotImplementedException();
		}

		public void Move(Vector2 direction, float speed)
		{
			var directionNormalized = direction.normalized;
			_bugView.Mover.Move(directionNormalized, speed);
			_bugView.Rotator.Rotate(directionNormalized);
		}
	}
}