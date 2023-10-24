using System;
using _Game._Hero.Scripts.Factory;
using _Game._Weapon._Projectile.Factory;
using _Game._Weapon.Scripts.Factory;
using _Game.Core.AssetManagment;
using _Game.Core.Services;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Input;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
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

        public BootstrapState(
            GameStateMachine stateMachine, 
            SceneLoader sceneLoader, 
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        
        public void Enter()
        {
            _sceneLoader.Load(Constants.Scenes.STARTUP, EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>( Constants.Scenes.LEVEL_1);

        private void RegisterServices()
        {
            _services.RegisterSingle<IRandomService>(new UnityRandomService());
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IWorldCameraService>(new WorldCameraService());
            _services.RegisterSingle<IVfxAudioSourceService>(new VfxAudioSourceService());


            RegisterStaticDataService();

            _services.RegisterSingle<IAssets>(new AssetProvider());

            RegisterVfxFactory();
            RegisterProjectileFactory();
            RegisterWeaponFactory();
            RegisterHeroFactory();
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
                _services.Single<IRandomService>());
            _services.RegisterSingle<IEnemyFactory>(enemyFactory);
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