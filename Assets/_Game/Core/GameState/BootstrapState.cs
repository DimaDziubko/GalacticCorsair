using System.Collections.Generic;
using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game._Weapon.Scripts.Factory;
using _Game.Core.AssetManagement;
using _Game.Core.Loading.Scripts;
using _Game.Core.Services;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Input;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
using _Game.PowerUp.Scripts.Factory;
using _Game.StaticData;
using _Game.Utils;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;

namespace _Game.Core.GameState
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly Camera _uiCamera;

        public BootstrapState(
            GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            AllServices services,
            Camera uiCamera)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _uiCamera = uiCamera;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(Constants.Scenes.STARTUP, EnterMenuState);
        }

        private void EnterMenuState()
        {
            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(_services.Single<IAssetProvider1>());
            loadingOperations.Enqueue(new MenuLoadingOperation(
                _sceneLoader,
                _stateMachine,
                _services.Single<IWorldCameraService>()));
            _stateMachine.Enter<MenuState, Queue<ILoadingOperation>>(loadingOperations);
        }

        public void Exit()
        {
            
        }
        
        private void RegisterServices()
        {
            _services.RegisterSingle<IRandomService>(new UnityRandomService());
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IWorldCameraService>(new WorldCameraService(_uiCamera));
            _services.RegisterSingle<ILoadingScreenProvider>(
                new LoadingScreenProvider(_services.Single<IWorldCameraService>()));
            _services.RegisterSingle<IVfxAudioSourceService>(new VfxAudioSourceService());


            RegisterStaticDataService();

            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IAssetProvider1>(new AssetProvider1());

            RegisterVfxFactory();
            RegisterProjectileFactory();
            RegisterWeaponFactory();
            RegisterHeroFactory();
            RegisterPowerUpFactory();
            RegisterEnemyFactory();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
        }

        private void RegisterVfxFactory()
        {
            IAssets assets = _services.Single<IAssets>();
            VfxFactory vfxFactory = assets.Load<VfxFactory>(AssetAddress.VFX_FACTORY_PATH);
            vfxFactory.Construct(_services.Single<IStaticDataService>());
            _services.RegisterSingle<IVfxFactory>(vfxFactory);
        }

        private void RegisterEnemyFactory()
        {
            IAssets assets = _services.Single<IAssets>();
            EnemyFactory enemyFactory = assets.Load<EnemyFactory>(AssetAddress.ENEMY_FACTORY_PATH);
            enemyFactory.Construct(
                _services.Single<IStaticDataService>(),
                _services.Single<IProjectileFactory>(),
                _services.Single<IWorldCameraService>(),
                _services.Single<IVfxAudioSourceService>(),
                _services.Single<IVfxFactory>(),
                _services.Single<IPowerUpFactory>(),
                _services.Single<IRandomService>());
            _services.RegisterSingle<IEnemyFactory>(enemyFactory);
        }

        private void RegisterPowerUpFactory()
        {
            IAssets assets = _services.Single<IAssets>();
            PowerUpFactory powerUpFactory = assets.Load<PowerUpFactory>(AssetAddress.POWER_UP_FACTORY_PATH);
            powerUpFactory.Construct(
                _services.Single<IStaticDataService>(),
                _services.Single<IWorldCameraService>(),
                _services.Single<IRandomService>());
            _services.RegisterSingle<IPowerUpFactory>(powerUpFactory);
        }
        
        private void RegisterProjectileFactory()
        {
            IAssets assets = _services.Single<IAssets>();
            ProjectileFactory projectileFactory = assets.Load<ProjectileFactory>(AssetAddress.PROJECTILE_FACTORY_PATH);
            projectileFactory.Construct(
                _services.Single<IStaticDataService>(),
                _services.Single<IWorldCameraService>(),
                _services.Single<IVfxFactory>(),
                _services.Single<IVfxAudioSourceService>());
            _services.RegisterSingle<IProjectileFactory>(projectileFactory);
        }

        private void RegisterStaticDataService()
        {
            StaticDataService staticData = new StaticDataService();
            staticData.LoadEnemies();
            staticData.LoadWeapons();
            staticData.LoadHeroes();
            staticData.LoadPowerUps();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }

        private void RegisterWeaponFactory()
        {
            IAssets assets = _services.Single<IAssets>();
            WeaponFactory weaponFactory = assets.Load<WeaponFactory>(AssetAddress.WEAPON_FACTORY_PATH);
            weaponFactory.Construct(
                _services.Single<IStaticDataService>(),
                _services.Single<IProjectileFactory>(),
                _services.Single<IVfxAudioSourceService>(),
                _services.Single<IVfxFactory>());
            _services.RegisterSingle<IWeaponFactory>(weaponFactory);
        }

        private void RegisterHeroFactory()
        {
            IAssets assets = _services.Single<IAssets>();
            HeroFactory heroFactory = assets.Load<HeroFactory>(AssetAddress.HERO_FACTORY_PATH);
            heroFactory.Construct(
                _services.Single<IInputService>(),
                _services.Single<IWeaponFactory>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IWorldCameraService>(),
                _services.Single<IVfxAudioSourceService>());
            _services.RegisterSingle<IHeroFactory>(heroFactory);
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
            {
                return new MobileInputService();
            }
        }
    }
}