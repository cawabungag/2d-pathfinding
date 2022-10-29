using UnityEngine;

namespace Core
{
	public class CanvasRoot : MonoBehaviour
	{
		[SerializeField] 
		private Canvas _canvas;
		
		public Transform Root => _canvas.transform;
	}
}