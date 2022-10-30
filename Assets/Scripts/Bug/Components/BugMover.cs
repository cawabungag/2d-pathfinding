using UnityEngine;
using Utils;

namespace Factories.Bug
{
	public class BugMover : MonoBehaviour
	{
		public void Move(Vector3 target, float speed, float deltaTime)
		{
			var oldPosition = transform.position;
			var targetPosition = new Vector3(target.x, target.y, Vector2IntExtensions.DEFAULT_Z_AXIS);
			transform.position = Vector3.MoveTowards(oldPosition, targetPosition, speed * deltaTime);
		}
	}
}