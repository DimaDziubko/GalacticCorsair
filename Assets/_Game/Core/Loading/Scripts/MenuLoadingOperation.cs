using System;
using _Game.Core.GameState;
using _Game.Core.Services.Camera;
using _Game.Utils;
using _Game.Utils.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Game.Core.Loading.Scripts
{
    public sealed class MenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Main menu loading...";
        
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly IWorldCameraService _cameraService;

        public MenuLoadingOperation(
            SceneLoader sceneLoader,
            GameStateMachine stateMachine,
            IWorldCameraService cameraService)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _cameraService = cameraService;
        }
        
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress.Invoke(0.5f);
            var loadOp = _sceneLoader.LoadSceneAsync(Constants.Scenes.MAIN_MENU, LoadSceneMode.Additive);
            while (loadOp.isDone == false)
            {
                await UniTask.Yield();
            }
            onProgress?.Invoke(0.7f);
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
