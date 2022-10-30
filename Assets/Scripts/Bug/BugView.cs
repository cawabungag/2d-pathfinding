using UnityEngine;
using UnityEngine.UI;

namespace Factories.Bug
{
	public class BugView : MonoBehaviour
	{
		[SerializeField] 
		private Image _image;
		
		[SerializeField] 
		private BugMover _bugMover;

		[SerializeField] 
		private BugRotator _bugRotator;

		public BugMover Mover => _bugMover;
		public BugRotator Rotator => _bugRotator;
		public Image Image => _image;
	}
}