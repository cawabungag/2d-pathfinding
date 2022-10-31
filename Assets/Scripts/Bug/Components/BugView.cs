using UnityEngine;

namespace Bug.Components
{
	public class BugView : MonoBehaviour
	{
		[SerializeField] 
		private BugMover _bugMover;

		[SerializeField] 
		private BugRotator _bugRotator;

		public BugMover Mover => _bugMover;
		public BugRotator Rotator => _bugRotator;
	}
}