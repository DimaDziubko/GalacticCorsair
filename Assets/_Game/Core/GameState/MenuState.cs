using System.Collections.Generic;
using _Game.Core.AssetManagement;
using _Game.Core.Loading.Scripts;
using _Game.Core.Services.Camera;
using Cysharp.Threading.Tasks;

namespace _Game.Core.GameState
{
    public class MenuState : IPayloadedState<Queue<ILoadingOperation>>
    {
        private readonly ILoadingScreenProvider _loadingProvider;

        public MenuState(
            ILoadingScreenProvider loadingProvider 
        )
        {
            _loadingProvider = loadingProvider;
        }

        public void Enter(Queue<ILoadingOperation> loadingOperations)
        {
            _loadingProvider.LoadAndDestroy(loadingOperations).Forget();
        }

        public void Exit()
        {
            
        }
        
    }
}
