using UnityEngine;

namespace Factories.Bug
{
	public interface IBugPresenter
	{
		void Initialize();
		void Move(Vector2 direction, float speed);
	}
}