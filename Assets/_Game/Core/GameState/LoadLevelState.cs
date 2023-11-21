using System.Collections.Generic;
using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game.Core.Loading.Scripts;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
using _Game.PowerUp.Scripts.Factory;
using _Game.Vfx.Scripts.Factory;
using Cysharp.Threading.Tasks;

namespace _Game.Core.GameState
{
    public class LoadLevelState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;

        private readonly IHeroFactory _heroFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IWorldCameraService _cameraService;
        private readonly IRandomService _randomService;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVfxFactory _vfxFactory;
        private readonly IPowerUpFactory _powerUpFactory;
        private readonly ILoadingScreenProvider _loadingProvider;

        //private readonly LoadingCurtain _curtain;
        //private readonly IGameFactory _gameFactory;
        //private readonly IPersistentProgressService _progressService;
        //private readonly IStaticDataService _staticData;
        //private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            IGameStateMachine stateMachine, 
            SceneLoader sceneLoader,
            IHeroFactory heroFactory,
            IWorldCameraService cameraService,
            IEnemyFactory enemyFactory,
            IRandomService randomService,
            IProjectileFactory projectileFactory,
            IVfxFactory vfxFactory,
            IPowerUpFactory powerUpFactory,
            ILoadingScreenProvider loadingProvider)
            // IGameFactory gameFactory,
            // IPersistentProgressService progressService,
            // IStaticDataService staticData
            // IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _heroFactory = heroFactory;
            _cameraService = cameraService;
            _enemyFactory = enemyFactory;
            _randomService = randomService;
            _projectileFactory = projectileFactory;
            _vfxFactory = vfxFactory;
            _powerUpFactory = powerUpFactory;
            _loadingProvider = loadingProvider;
            // _gameFactory =  gameFactory;
            // _progressService = progressService;
            // _staticData = staticData;
            // _uiFactory = uiFactory;

        }

        public void Enter()
        {
            // _gameFactory.Cleanup();
            // _gameFactory.WarmUp();
            Load();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }

        private void Load()
        {
            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(
                new LevelLoadingOperation(
                    _sceneLoader,
                    _heroFactory,
                    _cameraService,
                    _enemyFactory,
                    _randomService,
                    _projectileFactory,
                    _vfxFactory,
                    _powerUpFactory,
                    _stateMachine
                    ));
            _loadingProvider.LoadAndDestroy(loadingOperations).Forget();

            _stateMachine.Enter<GameLoopState>();
        }

        // private async Task InitUIRoot()
        // {
        //     await _uiFactory.CreateUIRoot();
        // }

        // private void InformProgressReaders()
        // {
        //     foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        //     {
        //         progressReader.LoadProgress(_progressService.Progress);
        //     }
        // }

        // private async Task InitGameWorld()
        // {
        //     LevelStaticData levelData = LevelStaticData();
        //
        //     await InitSpawners(levelData);
        //     await InitDroppedLoot();
        //     GameObject hero = await InitHero(levelData);
        //     await InitHud(hero);
        //     CameraFollow(hero);
        // }

        // private async Task InitSpawners(LevelStaticData levelData)
        // {
        //     foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
        //     {
        //         await _gameFactory.CreteSpawner(spawnerData.Position, spawnerData.Id, spawnerData.MonsterTypeId);
        //     }
        // }

        // private async Task InitDroppedLoot()
        // {
        //     DroppedLoot droppedLoot = _progressService.Progress.WorldData.DroppedLoot;
        //     foreach (DroppedItem drop in droppedLoot.Items)
        //     {
        //         LootPiece lootPiece = await _gameFactory.CreateLoot();
        //         lootPiece.transform.position = drop.Position.AsUnityVector();
        //         lootPiece.Initialize(drop.Loot);
        //     }
        //
        //     droppedLoot.Clear();
        // }
        //
        // private async Task<GameObject> InitHero(LevelStaticData levelData)
        // {
        //     return await _gameFactory.CreateHero(levelData.InitialHeroPosition);
        // }
        //
        // private async Task InitHud(GameObject hero)
        // {
        //     GameObject hud = await _gameFactory.CreateHud();
        //     
        //     hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        //
        // }
        //
        // private void CameraFollow(GameObject hero)
        // {
        //     Camera.main
        //         .GetComponent<CameraFollow>()
        //         .Follow(hero);
        // }
        //
        // private LevelStaticData LevelStaticData()
        // {
        //     string sceneKey = SceneManager.GetActiveScene().name;
        //     LevelStaticData levelData = _staticData.ForLevel(sceneKey);
        //     return levelData;
        // }
    }
}