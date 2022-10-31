using Core.Services;
using UnityEngine;

namespace Assets.ResourceLoader
{
	public interface IResourceLoader : IService
	{
		GameObject LoadGameObject(string path);
		TextAsset LoadTextAsset(string path);
	}
}