using System.Collections.Generic;
using _Game.Core.AssetManagement;
using _Game.Core.Services.Camera;
using Cysharp.Threading.Tasks;

namespace _Game.Core.Loading.Scripts
{
    public class LoadingScreenProvider : LocalAssetLoader, ILoadingScreenProvider
    {
        private readonly IWorldCameraService _cameraService;

        public LoadingScreenProvider(IWorldCameraService cameraService)
        {
            _cameraService = cameraService;
        }
        
        public async UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations)
        {
            var loadingScreen = await Load<LoadingScreen>(AssetsConstants.LOADING_SCREEN);
            await loadingScreen.Load(loadingOperations,
                _cameraService.UICamera);
            Unload();
        }
    }
}