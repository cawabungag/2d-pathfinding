using UnityEngine;

namespace Bug.Components
{
	public class BugRotator : MonoBehaviour
	{
		public void Rotate(Vector2 direction) 
			=> transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}
}