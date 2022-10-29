using UnityEngine;

namespace Assets
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
	}
}