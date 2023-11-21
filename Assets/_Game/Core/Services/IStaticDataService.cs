using _Game._Hero.Scripts;
using _Game._Weapon.Scripts;
using _Game.Enemies.Scripts;
using _Game.PowerUp.Scripts;

namespace _Game.Core.Services
{
    namespace _Game.StaticData
    {
        public interface IStaticDataService : IService
        {
            WeaponConfig ForWeapon(WeaponType type);
            EnemyConfig ForEnemy(EnemyType type);
            HeroConfig ForHero(HeroType type);
            PowerUpConfig ForPowerUp((PowerUpType, WeaponType) type);
            PowerUpConfig[] ForPowerUps { get; }
        }
    }
}