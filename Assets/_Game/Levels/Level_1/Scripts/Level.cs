using System;
using System.Collections.Generic;
using _Game._Hero.Scripts;
using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game.Common;
using _Game.Core;
using _Game.Core.Factory;
using _Game.Core.GameState;
using _Game.Core.Loading.Scripts;
using _Game.Core.Scenario;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts;
using _Game.Enemies.Scripts.Factory;
using _Game.GameResult.Scripts;
using _Game.PowerUp.Scripts.Factory;
using _Game.UI.Hud.Scripts;
using _Game.Utils;
using _Game.Vfx.Scripts.Factory;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.Levels.Level_1.Scripts
{
    public class Level : MonoBehaviour, ILevelCleaner
    {
        public IEnumerable<GameObjectFactory> Factories { get; private set; }
        public string SceneName => Constants.Scenes.LEVEL_1;
        
        private IHeroFactory _heroFactory;
        private IEnemyFactory _enemyFactory;
        private IWorldCameraService _cameraService;
        private IRandomService _randomService;
        private IProjectileFactory _projectileFactory;
        private IVfxFactory _vfxFactory;
        private IPowerUpFactory _powerUpFactory;
        private SceneLoader _sceneLoader;
        private IGameStateMachine _stateMachine;

        [SerializeField] private GameScenario _scenario;
        [SerializeField] private Hud _hud;
        [SerializeField] private LevelCompleteWindow _levelCompleteWindow;

        private Hero _hero;

        private readonly GameBehaviourCollection _enemies = new GameBehaviourCollection();
        private GameBehaviourCollection ProjectileContainer => _projectileFactory.ProjectileContainer;
        private GameBehaviourCollection PowerUpContainer => _powerUpFactory.PowerUpContainer;
        private GameBehaviourCollection VfxEntities => _vfxFactory.VfxEntitiesContainer;

        private bool _scenarioInProcess;
        private GameScenario.State _activeScenario;


        public void Initialize(
            IHeroFactory heroFactory,
            IWorldCameraService cameraService,
            IEnemyFactory enemyFactory,
            IRandomService randomService,
            IProjectileFactory projectileFactory,
            IVfxFactory vfxFactory,
            IPowerUpFactory powerUpFactory,
            SceneLoader sceneLoader,
            IGameStateMachine stateMachine)
        {
            _heroFactory = heroFactory;
            _cameraService = cameraService;
            _enemyFactory = enemyFactory;
            _randomService = randomService;
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
            _powerUpFactory = powerUpFactory;
            _hud.Initialize(_cameraService);
            _levelCompleteWindow.Initialize(_cameraService);
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            
            Factories = new GameObjectFactory[]
            {
                heroFactory as GameObjectFactory, 
                enemyFactory as GameObjectFactory, 
                projectileFactory as GameObjectFactory, 
                vfxFactory as GameObjectFactory, 
                powerUpFactory as GameObjectFactory, 
            };
        }

        public void BeginGame()
        {
            Cleanup();
            SpawnHero(HeroType.Hawk);
            
            _hud.Reset();
            _hero.HealthChanged += _hud.UpdatePlayerHealth;
            _hero.SubscribeToShieldCapacityChange(_hud.UpdateShieldCapacity);

            _activeScenario = _scenario.Begin(this);
            _scenarioInProcess = true;
        }

        private void Update()
        {
            Debug.Log(ProjectileContainer.IsEmpty);
            
            if (_scenarioInProcess)
            {
                if (_activeScenario.Progress() == false && _enemies.IsEmpty)
                {
                    _scenarioInProcess = false;
                    _levelCompleteWindow.Show(BeginGame, GoToMainMenu);
                }
                
                if (_hero != null)
                {
                    _hero.GameUpdate(); 
                }
                _enemies.GameUpdate();
                ProjectileContainer.GameUpdate();
                PowerUpContainer.GameUpdate();
                VfxEntities.GameUpdate();
                
                if (_hero.IsDead)
                {
                    _hero.HealthChanged -= _hud.UpdatePlayerHealth;
                    _hero.UnSubscribeToShieldCapacityChange(_hud.UpdateShieldCapacity);
                }
            }
        }

        public void Cleanup()
        {
            if(_hero) _hero.Recycle();
            _scenarioInProcess = false;
            _enemies.Clear();
            ProjectileContainer.Clear();
            PowerUpContainer.Clear();
            VfxEntities.Clear();
        }
        
        private void GoToMainMenu()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new ClearGameOperation(
                _sceneLoader, 
                this,
                _cameraService,
                _stateMachine));
            _stateMachine.Enter<MenuState, Queue<ILoadingOperation>>(operations);
        }

        private void LateUpdate()
        {
            _hero.GameLateUpdate();
            _enemies.GameLateUpdate();
            ProjectileContainer.GameLateUpdate();
        }

        private void SpawnHero(HeroType type)
        {
            _hero = _heroFactory.Get(type);
            _hero.gameObject.transform.rotation = quaternion.Euler(-90, 0, 0);
        }
        
        public void SpawnEnemy(EnemyType type)
        {
            Enemy enemy = _enemyFactory.Get(type);

            float enemyPadding = enemy.Radius;

            enemy.Position = ChooseSpawnPosition(enemyPadding);
            _enemies.Add(enemy);
        }

        private Vector3 ChooseSpawnPosition(float enemyPadding)
        {
            Direction directionFrom = ChooseRandomDirection();
            
            float xMin = -_cameraService.CameraWidth - enemyPadding;
            float xMax = _cameraService.CameraWidth + enemyPadding;
            float yMin = -_cameraService.CameraHeight - enemyPadding;
            float yMax = _cameraService.CameraHeight + enemyPadding;

            switch (directionFrom)
            {
                case Direction.North:
                    return  new Vector3(_randomService.Next(xMin, xMax), yMax, 0);
                case Direction.East:
                    return  new Vector3(xMax, _randomService.Next(yMin, yMax), 0);
                case Direction.South:
                    return  new Vector3(_randomService.Next(xMin, xMax), yMin, 0);
                case Direction.West:
                    return new Vector3(xMin, _randomService.Next(yMin, yMax), 0);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Direction ChooseRandomDirection()
        {
            return (Direction)_randomService.Next(0, 3);
        }

        enum Direction
        {
            North,
            East,
            South,
            West
        }
        
    }
}