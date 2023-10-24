using UnityEngine;

namespace _Game.Common
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        public virtual bool GameUpdate() => true;

        public abstract void GameLateUpdate();
        public abstract void Recycle();
    }
}