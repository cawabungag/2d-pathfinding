using Core;
using UnityEngine;

namespace Assets
{
	public interface IInstantiator : IService
	{
		GameObject InstantiateGameObject(string path, Vector2 position, Quaternion rotation);
	}
}