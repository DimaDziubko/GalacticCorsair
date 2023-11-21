using System;
using _Game.Core.GameState;
using _Game.Core.Services.Camera;
using _Game.Levels.Level_1.Scripts;
using _Game.Utils;
using _Game.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Game.Core.Loading.Scripts
{
    public class ClearGameOperation : ILoadingOperation
    {
        public string Description => "Clearing...";

        private readonly ILevelCleaner _levelCleanUp;
        private readonly SceneLoader _sceneLoader;
        private readonly IWorldCameraService _cameraService;
        private readonly IGameStateMachine _stateMachine;

        public ClearGameOperation(
            SceneLoader sceneLoader,
            ILevelCleaner levelCleanUp,
            IWorldCameraService cameraService,
            IGameStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _levelCleanUp = levelCleanUp;
            _cameraService = cameraService;
            _stateMachine = stateMachine;
        }
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.2f);
            _levelCleanUp.Cleanup();

            foreach (var factory in _levelCleanUp.Factories)
            {
                await factory.Unload();
            }
            onProgress?.Invoke(0.5f);

            var loadOp = _sceneLoader.LoadSceneAsync(Constants.Scenes.MAIN_MENU, LoadSceneMode.Additive);

            while (loadOp.isDone == false)
            {
                await UniTask.Yield();
            }
            onProgress?.Invoke(0.75f);

            var unloadOp = _sceneLoader.UnloadSceneAsync(_levelCleanUp.SceneName);

            while (unloadOp.isDone == false)
            {
                await UniTask.Yield();
            }
            
            onProgress?.Invoke(0.6f);
            
            Scene scene = _sceneLoader.GetSceneByName(Constants.Scenes.MAIN_MENU);
            var level = scene.GetRoot<MainMenu>();
            
            onProgress?.Invoke(0.85f);
            level.Initialize(
                _stateMachine,
                _cameraService);

            onProgress?.Invoke(1f);
        }
    }
}