using Core;
using UnityEngine;

namespace Assets
{
	public interface IResourceLoader : IService
	{
		GameObject LoadGameObject(string path);
	}
}