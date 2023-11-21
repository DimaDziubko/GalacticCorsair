using System;
using _Game.Core.Loading.Scripts;
using _Game.Core.Services;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace _Game.Core.AssetManagement
{
    public interface IAssetProvider1 : IService, ILoadingOperation
    {
        UniTask<SceneInstance> LoadSceneAdditive(string sceneId);
        UniTask UnloadAdditiveScene(SceneInstance scene);
        string Description { get; }
        UniTask Load(Action<float> onProgress);
    }
}