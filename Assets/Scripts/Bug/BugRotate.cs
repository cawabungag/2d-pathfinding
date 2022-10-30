using UnityEngine;

namespace Factories.Bug
{
	public class BugRotate : MonoBehaviour
	{
		public void Rotate(Vector2 direction)
		{
			transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
		}
	}
}