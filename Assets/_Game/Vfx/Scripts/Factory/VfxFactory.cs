using _Game._Weapon.Scripts;
using _Game.Common;
using _Game.Core.Factory;
using _Game.Core.Services._Game.StaticData;
using _Game.Enemies.Scripts;
using UnityEngine;

namespace _Game.Vfx.Scripts.Factory
{
    [CreateAssetMenu(fileName = "VfxFactory", menuName = "Factories/Vfx Factory")]
    public class VfxFactory : GameObjectFactory, IVfxFactory
    {
        public GameBehaviourCollection VfxEntitiesContainer => _vfxEntitiesContainer;
        
        private IStaticDataService _staticData;


        public void Construct(
            IStaticDataService staticData)
        {
            _staticData = staticData;
        }
        
        private readonly GameBehaviourCollection _vfxEntitiesContainer = new GameBehaviourCollection();

        public Impact GetImpact(WeaponType type) 
        {
            WeaponConfig weaponConfig = _staticData.ForWeapon(type);
            Impact instance = Get(weaponConfig.ImpactPrefab, weaponConfig.ImpactDuration);
            return instance;
        }
        
        public Explosion GetExplosion(EnemyType type) 
        {
            EnemyConfig enemyConfig = _staticData.ForEnemy(type);
            Explosion instance = Get(enemyConfig.ExplosionPrefab, enemyConfig.ExplosionDuration);
            return instance;
        }

        public MuzzleFlash GetMuzzleFlash(WeaponType type)
        {
            WeaponConfig weaponConfig = _staticData.ForWeapon(type);
            MuzzleFlash instance = Get(weaponConfig.MuzzleFlash, weaponConfig.MuzzleDuration);
            return instance;
        }
        
        private T Get<T>(T prefab, float duration) where T : VfxEntity
        {
            T instance = CreateGameObjectInstance(prefab);
            instance.OriginFactory = this;
            instance.Duration = duration;
            _vfxEntitiesContainer.Add(instance);
            return instance;
        }

        public void Reclaim (VfxEntity entity) => 
            Destroy(entity.gameObject);
    }
}