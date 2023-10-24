using _Game.Core.GameState;
using _Game.Core.Services;

namespace _Game.Core
{
    public class Game
    {
        public readonly GameStateMachine StateMachine; 

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container );
        }
        
    }
}