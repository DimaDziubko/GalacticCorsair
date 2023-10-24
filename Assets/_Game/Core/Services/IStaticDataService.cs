using _Game._Hero.Scripts;
using _Game._Weapon.Scripts;
using _Game.Enemies.Scripts;

namespace _Game.Core.Services
{
    namespace _Game.StaticData
    {
        public interface IStaticDataService : IService
        {
            WeaponConfig ForWeapon(WeaponType type);
            EnemyConfig ForEnemy(EnemyType type);
            HeroConfig ForHero(HeroType type);
        }
    }
}