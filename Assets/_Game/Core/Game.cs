using _Game.Core.GameState;
using _Game.Core.Services;
using UnityEngine;

namespace _Game.Core
{
    public class Game
    {
        public readonly GameStateMachine StateMachine; 

        public Game(ICoroutineRunner coroutineRunner, Camera uiCamera)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, uiCamera);
        }
        
    }
}