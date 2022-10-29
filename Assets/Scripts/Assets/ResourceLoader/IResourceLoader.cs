using UnityEngine;

namespace Assets
{
	public interface IResourceLoader
	{
		GameObject LoadGameObject(string path);
	}
}