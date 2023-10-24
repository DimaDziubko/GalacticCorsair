﻿using System.Threading.Tasks;
using _Game._Weapon.Scripts;
using _Game.Vfx.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Core.Factory
{
    public abstract class GameObjectFactory : ScriptableObject
    {
        private Scene _scene;

        protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
        {
            GetOrCreateScene();

            T instance = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);
            return instance;
        }
        
        protected T CreateGameObjectInstance<T>(T prefab, Transform parent) where T : MonoBehaviour
        {
            T instance = Instantiate(prefab, parent);
            return instance.GetComponent<T>();
        }

        public async Task Unload()
        {
            if (_scene.isLoaded)
            {
                var unloadOp = SceneManager.UnloadSceneAsync(_scene);
                while (unloadOp.isDone == false)
                {
                    await Task.Delay(1);
                }
            }
        }

        private void GetOrCreateScene() 
        {
            if (_scene.isLoaded)
            {
                if (Application.isEditor)
                {
                    _scene = SceneManager.GetSceneByName(name);
                    if (!_scene.isLoaded)
                    {
                        _scene = SceneManager.CreateScene(name);
                    }
                }
            }
            else
            {
                _scene = SceneManager.CreateScene(name);
            }
        }
        
    }
}