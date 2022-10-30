using UnityEngine;

namespace Factories.Bug
{
	public class BugPresenter : IBugPresenter
	{
		private readonly BugView _bugView;

		public BugPresenter(BugView bugView) 
			=> _bugView = bugView;

		public void Move(Vector3 target, float speed, float deltaTime)
		{
			_bugView.Mover.Move(target, speed, deltaTime);
			var direction = target.normalized;
			_bugView.Rotator.Rotate(direction);
		}

		public Vector2 Position => _bugView.transform.position;
	}
}