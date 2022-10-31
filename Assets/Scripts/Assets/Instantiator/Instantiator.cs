using Assets.ResourceLoader;
using UnityEngine;

namespace Assets.Instantiator
{
	public class Instantiator : IInstantiator
	{
		private readonly IResourceLoader _resourceLoader;

		public Instantiator(IResourceLoader resourceLoader)
		{
			_resourceLoader = resourceLoader;
		}
		
		public GameObject InstantiateGameObject(string path, Vector2 position, Quaternion rotation)
		{
			var asset = _resourceLoader.LoadGameObject(path);
			return Object.Instantiate(asset, position, rotation);
		}

		public GameObject InstantiateGameObject(string path, Transform parent)
		{
			var asset = _resourceLoader.LoadGameObject(path);
			return Object.Instantiate(asset, parent);
		}
	}
}