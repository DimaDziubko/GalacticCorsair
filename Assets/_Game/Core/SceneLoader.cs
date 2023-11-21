using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Core
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene =  SceneManager.LoadSceneAsync(nextScene);
            
            while (waitNextScene.isDone == false)
            {
                yield return null;
            }
            
            onLoaded?.Invoke();
        }

        public AsyncOperation LoadSceneAsync(string name, LoadSceneMode mode)
        {
            var loadOp = SceneManager.LoadSceneAsync(name, mode);
            return loadOp;
        }

        public Scene GetSceneByName(string name) => 
            SceneManager.GetSceneByName(name);

        public AsyncOperation UnloadSceneAsync(string sceneName) => 
            SceneManager.UnloadSceneAsync(sceneName);
    }
}