using _Game._Weapon.Scripts;
using _Game.Common;
using _Game.Core.Services;
using _Game.Enemies.Scripts;

namespace _Game.Vfx.Scripts.Factory
{
    public interface IVfxFactory : IService
    {
        GameBehaviourCollection VfxEntitiesContainer { get; }
        public Impact GetImpact(WeaponType type);
        public MuzzleFlash GetMuzzleFlash(WeaponType type);
        public Explosion GetExplosion(EnemyType type);
    }
}