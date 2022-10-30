using UnityEngine;

namespace InputService
{
	public class InputService : IInputService
	{
		Vector2 IInputService.GetMousePosition
		{
			get
			{
				var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				return Camera.main.ScreenToWorldPoint(screenPosition);
			}
		}
	}
}