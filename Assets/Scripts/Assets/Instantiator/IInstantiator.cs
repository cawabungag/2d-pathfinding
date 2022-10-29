using UnityEngine;

namespace Assets
{
	public interface IInstantiator
	{
		GameObject InstantiateGameObject(string path, Vector2 position, Quaternion rotation);
	}
}