using _Game._Weapon._Projectile.Factory;
using _Game.Core.Factory;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Audio;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;
using UnityEngine.VFX;

namespace _Game._Weapon.Scripts.Factory
{
    [CreateAssetMenu(fileName = "WeaponFactory", menuName = "Factories/Weapon Factory")]
    public class WeaponFactory : GameObjectFactory, IWeaponFactory
    {
        private IStaticDataService _staticData;
        private IProjectileFactory _projectileFactory;
        private IVfxAudioSourceService _audioSourceService;
        private IVfxFactory _vfxFactory;

        public void Construct(
            IStaticDataService staticData, 
            IProjectileFactory projectileFactory,
            IVfxAudioSourceService audioSourceService,
            IVfxFactory vfxFactory)
        {
            _staticData = staticData;
            _projectileFactory = projectileFactory;
            _audioSourceService = audioSourceService;
            _vfxFactory = vfxFactory;
        }


        public Weapon Get(WeaponType type, Transform parent)
        {
            WeaponConfig weaponConfig = _staticData.ForWeapon(type);
            Weapon instance = CreateGameObjectInstance(weaponConfig.WeaponPrefab, parent);
            
            instance.Construct(
                _projectileFactory, 
                weaponConfig,
                _audioSourceService,
                _vfxFactory);
            instance.OriginFactory = this;
            
            return instance;
        }

        public void Reclaim (Weapon weapon) => 
            Destroy(weapon.gameObject);
    }
}