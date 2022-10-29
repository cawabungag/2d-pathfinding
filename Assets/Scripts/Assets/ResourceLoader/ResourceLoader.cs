using UnityEngine;

namespace Assets
{
	public class ResourceLoader : IResourceLoader
	{
		public GameObject LoadGameObject(string path) => Load<GameObject>(path);
		public TextAsset LoadTextAsset(string path) => Load<TextAsset>(path);
		private T Load<T>(string path) where T : Object => Resources.Load<T>(path);
	}
}