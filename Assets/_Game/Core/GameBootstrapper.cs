using _Game.Core.GameState;
using _Game.Core.Services.Audio;
using UnityEngine;

namespace _Game.Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        
        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}