using System.Collections.Generic;
using _Game.Core.Services;
using Cysharp.Threading.Tasks;

namespace _Game.Core.Loading.Scripts
{
    public interface ILoadingScreenProvider : IService
    {
        UniTask LoadAndDestroy(Queue<ILoadingOperation> loadingOperations);
    }
}