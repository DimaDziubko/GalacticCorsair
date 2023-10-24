using System;
using System.Collections.Generic;
using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game.Core.Services;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
using _Game.Vfx.Scripts.Factory;

namespace _Game.Core.GameState
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this, 
                    sceneLoader, 
                    services.Single<IHeroFactory>(),
                    services.Single<IWorldCameraService>(),
                    services.Single<IEnemyFactory>(),
                    services.Single<IRandomService>(),
                    services.Single<IProjectileFactory>(),
                    services.Single<IVfxFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
             _states[typeof(TState)] as TState;
        
    }
}