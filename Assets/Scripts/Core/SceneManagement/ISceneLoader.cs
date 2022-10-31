using System;

namespace Core.SceneManagement
{
	public interface ISceneLoader
	{
		void Load(string name, Action onLoaded = null);
	}
}