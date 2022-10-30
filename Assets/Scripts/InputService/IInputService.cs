using Core;
using UnityEngine;

namespace InputService
{
	public interface IInputService : IService
	{
		Vector2 GetMousePosition { get; }
	}
}