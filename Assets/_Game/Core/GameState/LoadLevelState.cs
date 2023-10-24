using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
using _Game.Levels.Level_1.Scripts;
using _Game.Utils;
using _Game.Utils.Extensions;
using _Game.Vfx.Scripts.Factory;
using UnityEngine.SceneManagement;

namespace _Game.Core.GameState
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        private readonly IHeroFactory _heroFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IWorldCameraService _cameraService;
        private readonly IRandomService _randomService;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IVfxFactory _vfxFactory;
        
        //private readonly LoadingCurtain _curtain;
        //private readonly IGameFactory _gameFactory;
        //private readonly IPersistentProgressService _progressService;
        //private readonly IStaticDataService _staticData;
        //private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine stateMachine, 
            SceneLoader sceneLoader,
            IHeroFactory heroFactory,
            IWorldCameraService cameraService,
            IEnemyFactory enemyFactory,
            IRandomService randomService,
            IProjectileFactory projectileFactory,
            IVfxFactory vfxFactory)
            // LoadingCurtain curtain, 
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
            // _curtain = curtain;
            // _gameFactory =  gameFactory;
            // _progressService = progressService;
            // _staticData = staticData;
            // _uiFactory = uiFactory;

        }

        public void Enter(string sceneName)
        {
            // _curtain.Show();
            // _gameFactory.Cleanup();
            // _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            // _curtain.Hide();
        }

        private void OnLoaded()
        {
            
            Scene scene = SceneManager.GetSceneByName(Constants.Scenes.LEVEL_1);
            Level level = scene.GetRoot<Level>();

            level.Initialize(
                _heroFactory,
                _cameraService,
                _enemyFactory,
                _randomService,
                _projectileFactory,
                _vfxFactory);
            level.BeginGame();

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