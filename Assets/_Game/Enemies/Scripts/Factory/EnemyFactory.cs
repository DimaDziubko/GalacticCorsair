using System;
using _Game._Weapon._Projectile.Factory;
using _Game.Core.Factory;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;

namespace _Game.Enemies.Scripts.Factory
{
    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factories/Enemy Factory")]
    public class EnemyFactory : GameObjectFactory, IEnemyFactory
    {
        private IStaticDataService _staticData;
        private IProjectileFactory _projectileFactory;
        private IWorldCameraService _cameraService;
        private IVfxAudioSourceService _audioSourceService;
        private IVfxFactory _vfxFactory;
        private IRandomService _randomService;

        public void Construct(
            IStaticDataService staticData, 
            IProjectileFactory projectileFactory,
            IWorldCameraService cameraService,
            IVfxAudioSourceService audioSourceService,
            IVfxFactory vfxFactory,
            IRandomService randomService)
        {
            _staticData = staticData;
            _projectileFactory = projectileFactory;
            _cameraService = cameraService;
            _audioSourceService = audioSourceService;
            _vfxFactory = vfxFactory;
            _randomService = randomService;
        }
        
        public Enemy Get(EnemyType type)
        {
            EnemyConfig config = _staticData.ForEnemy(type);
            
            Enemy instance = CreateGameObjectInstance(config.EnemyPrefab);
            instance.Construct(
                type,
                config.Speed.RandomValueInRange,
                config.Health.RandomValueInRange,
                config.ImpactSound,
                _cameraService,
                _audioSourceService,
                _vfxFactory,
                _randomService);
            instance.OriginFactory = this;
            return instance;
        }

        public void Reclaim (Enemy enemy) => 
            Destroy(enemy.gameObject);
    }
}
