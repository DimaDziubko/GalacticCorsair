using UnityEngine;

namespace _Game.Core.AssetManagment
{
    public class AssetProvider : IAssets
    {
        public T Load<T>(string path) where T : ScriptableObject
        {
             return Resources.Load(path) as T;
        }
    }
}