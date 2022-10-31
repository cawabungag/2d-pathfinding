using UnityEngine;
using Utils;

namespace Bug.Components
{
	public class BugRotator : MonoBehaviour
	{
		public void Rotate(Vector2 target)
		{
			var targetPosition = target.ToVector3();
			if (targetPosition == transform.position)
				return;

			var relativePos = targetPosition - transform.position;
			transform.rotation = Quaternion.LookRotation(relativePos);
		}
	}
}