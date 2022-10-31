using System;
using System.Collections;
using Core.Services;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
    public class SceneLoaderService : ISceneLoader, IService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoaderService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }
        
        private IEnumerator LoadScene(string name, Action onLoaded = null)
        {
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene.name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            var waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}