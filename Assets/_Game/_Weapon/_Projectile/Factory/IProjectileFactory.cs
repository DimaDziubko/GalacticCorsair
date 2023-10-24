using _Game._Weapon._Projectile.Scripts;
using _Game._Weapon.Scripts;
using _Game.Common;
using _Game.Core.Services;

namespace _Game._Weapon._Projectile.Factory
{
    public interface IProjectileFactory : IService
    {
        GameBehaviourCollection ProjectileContainer { get; }
        Projectile Get(WeaponType type);
        void Reclaim (Projectile projectile);
    }
}