using System;
using _Game.Core.Loading.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _Game.Core.AssetManagement
{
    public class AssetProvider1 : IAssetProvider1
    {
        private bool _isReady;

        private async UniTask WaitUntilReady()
        {
            while (_isReady == false)
            {
                await UniTask.Yield();
            }
        }

        public async UniTask<SceneInstance> LoadSceneAdditive(string sceneId)
        {
            await WaitUntilReady();
            var op = Addressables.LoadSceneAsync(sceneId,
                LoadSceneMode.Additive);
            return await op.Task;
        }

        public async UniTask UnloadAdditiveScene(SceneInstance scene)
        {
            await WaitUntilReady();
            var op = Addressables.UnloadSceneAsync(scene);
            await op.Task;
        }
        public string Description { get; }
        public async UniTask Load(Action<float> onProgress)
        {
            var operation = Addressables.InitializeAsync();
            await operation.Task;
            _isReady = true;
        }
    }
}
