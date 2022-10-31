using Core.Services;
using UnityEngine;

namespace Assets.Instantiator
{
	public interface IInstantiator : IService
	{
		GameObject InstantiateGameObject(string path, Vector2 position, Quaternion rotation);
		GameObject InstantiateGameObject(string path, Transform parent);
	}
}