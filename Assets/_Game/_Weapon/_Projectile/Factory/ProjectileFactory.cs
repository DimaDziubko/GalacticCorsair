using _Game._Weapon._Projectile.Scripts;
using _Game._Weapon.Scripts;
using _Game.Common;
using _Game.Core.Factory;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;

namespace _Game._Weapon._Projectile.Factory
{
    [CreateAssetMenu(fileName = "ProjectileFactory", menuName = "Factories/Projectile Factory")]
    public class ProjectileFactory : GameObjectFactory, IProjectileFactory
    {
        private IStaticDataService _staticData;
        private IWorldCameraService _cameraService;
        private IVfxFactory _vfxFactory;

        private readonly GameBehaviourCollection _projectileContainer = new GameBehaviourCollection();
        private IVfxAudioSourceService _audioSourceService;

        public GameBehaviourCollection ProjectileContainer => _projectileContainer;
        
        public void Construct(
            IStaticDataService staticData,
            IWorldCameraService cameraService,
            IVfxFactory vfxFactory,
            IVfxAudioSourceService audioSourceService)
        {
            _staticData = staticData;
            _cameraService = cameraService;
            _vfxFactory = vfxFactory;
            _audioSourceService = audioSourceService;
        }


        public Projectile Get(WeaponType type)
        {
            WeaponConfig weaponConfig = _staticData.ForWeapon(type);
            Projectile instance = CreateGameObjectInstance(weaponConfig.ProjectilePrefab);
            instance.Construct(
                type,
                weaponConfig.DamageOnHit,
                _cameraService,
                _vfxFactory,
                _audioSourceService,
                weaponConfig.HitSound);
            instance.OriginFactory = this;
            _projectileContainer.Add(instance);
            return instance;
        }

        public void Reclaim (Projectile projectile) => 
            Destroy(projectile.gameObject);
        
    }
}