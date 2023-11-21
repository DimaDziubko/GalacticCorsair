using System;
using Cysharp.Threading.Tasks;

namespace _Game.Core.Loading.Scripts
{
    public interface ILoadingOperation
    {
        public string Description { get; }
        public UniTask Load(Action<float> onProgress);
    }
}
