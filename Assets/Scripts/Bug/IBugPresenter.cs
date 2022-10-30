using UnityEngine;

namespace Factories.Bug
{
	public interface IBugPresenter
	{
		void Move(Vector3 target, float speed, float deltaTime);
		Vector2 Position { get; }
	}
}