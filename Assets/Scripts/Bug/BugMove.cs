using UnityEngine;

namespace Factories.Bug
{
	public class BugMove : MonoBehaviour
	{
		public void Move(Vector2 direction, float speed)
		{
			var oldPosition = transform.position;
			var delta = direction * Time.smoothDeltaTime * speed;
			var newPosition = oldPosition + new Vector3(delta.x, delta.y, 0f);
			transform.position = newPosition;
		}
	}
}