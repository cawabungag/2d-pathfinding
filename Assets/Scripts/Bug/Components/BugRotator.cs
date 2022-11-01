using UnityEngine;
using Utils;

namespace Bug.Components
{
	public class BugRotator : MonoBehaviour
	{
		public void Rotate(Vector2 target)
		{
			transform.rotation = Quaternion.LookRotation(transform.position - target.ToVector3(), Vector3.forward);
		}
	}
}