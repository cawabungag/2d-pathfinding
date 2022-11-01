using Core.Services;
using UnityEngine;

namespace InputService
{
	public interface IInputService : IService
	{
		Vector2 GetMousePosition { get; }
	}
}