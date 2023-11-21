using System;
using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game.Core.GameState;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
using _Game.Levels.Level_1.Scripts;
using _Game.PowerUp.Scripts.Factory;
using _Game.Utils;
using _Game.Utils.Extensions;
using _Game.Vfx.Scripts.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Game.Core.Loading.Scripts
{
    public sealed class LevelLoadingOperation : ILoadingOperation
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IHeroFactory _heroFactory;
        private readonly IWorldCameraService _cameraService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IRandomService _randomService;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVfxFactory _vfxFactory;
        private readonly IPowerUpFactory _powerUpFactory;
        private readonly IGameStateMachine _stateMachine;

        public string Description => "Game loading...";

        public LevelLoadingOperation(
            SceneLoader sceneLoader,
            IHeroFactory heroFactory,
            IWorldCameraService cameraService,
            IEnemyFactory enemyFactory,
            IRandomService randomService,
            IProjectileFactory projectileFactory,
            IVfxFactory vfxFactory,
            IPowerUpFactory powerUpFactory,
            IGameStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _heroFactory = heroFactory;
            _cameraService = cameraService;
            _enemyFactory = enemyFactory;
            _randomService = randomService;
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
            _powerUpFactory = powerUpFactory;
            _stateMachine = stateMachine;
        }
        public async UniTask Load(Action<float> onProgress)
        {
            onProgress?.Invoke(0.5f);
            var loadOp = _sceneLoader.LoadSceneAsync(Constants.Scenes.LEVEL_1,
                LoadSceneMode.Single);
            while (loadOp.isDone == false)
            {
                await UniTask.Yield();
            }
            onProgress?.Invoke(0.7f);
            
            Scene scene = _sceneLoader.GetSceneByName(Constants.Scenes.LEVEL_1);
            var level = scene.GetRoot<Level>();
            onProgress?.Invoke(0.85f);
            level.Initialize(
                _heroFactory,
                _cameraService,
                _enemyFactory,
                _randomService,
                _projectileFactory,
                _vfxFactory,
                _powerUpFactory,
                _sceneLoader,
                _stateMachine);
            level.BeginGame();
            onProgress?.Invoke(1.0f);
        }
    }
}