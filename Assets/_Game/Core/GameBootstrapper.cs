using _Game.Core.GameState;
using UnityEngine;

namespace _Game.Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Camera _uiCamera;
        
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(this, _uiCamera);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}