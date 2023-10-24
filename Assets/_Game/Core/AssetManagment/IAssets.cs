using _Game.Core.Services;
using UnityEngine;

namespace _Game.Core.AssetManagment
{
    public interface IAssets : IService
    {
        T Load<T>(string path) where T : ScriptableObject;
    }
}