using System.Collections.Generic;
using System.Linq;
using _Game._Hero.Scripts;
using _Game._Weapon.Scripts;
using _Game.Core.Services._Game.StaticData;
using _Game.Enemies.Scripts;
using _Game.PowerUp.Scripts;
using UnityEngine;

namespace _Game.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WEAPON_STATIC_DATA_PATH = "Weapons";
        private const string ENEMY_STATIC_DATA_PATH = "Enemies/EnemyGeneralConfig";
        private const string HERO_STATIC_DATA_PATH = "Heroes/HeroGeneralConfig";
        private const string POWER_UP_STATIC_DATA_PATH = "PowerUps/PowerUpConfig"; 
        
        private Dictionary<WeaponType, WeaponConfig> _weapons;
        private Dictionary<EnemyType, EnemyConfig> _enemies;
        private Dictionary<HeroType, HeroConfig> _heroes;
        private Dictionary<(PowerUpType, WeaponType), PowerUpConfig> _powerUps;

        public void LoadHeroes()
        {
            var generalHeroConfig = Resources.Load<HeroGeneralConfig>(HERO_STATIC_DATA_PATH);
            _heroes = generalHeroConfig.Configs.ToDictionary(x => x.Type, x => x);
        }
        
        public void LoadWeapons()
        {
            _weapons = Resources.LoadAll<WeaponConfig>(WEAPON_STATIC_DATA_PATH)
                .ToDictionary(x=>x.Type, x => x);
        }
        
        public void LoadEnemies()
        {
            var generalEnemyConfig = Resources.Load<EnemyGeneralConfig>(ENEMY_STATIC_DATA_PATH);
            _enemies = generalEnemyConfig.Configs.ToDictionary(x => x.Type, x => x);
        }
        
        public void LoadPowerUps()
        {
            var generalPowerUpConfig = Resources.Load<PowerUpGeneralConfig>(POWER_UP_STATIC_DATA_PATH);
            _powerUps = generalPowerUpConfig.Configs.ToDictionary( x => (x.Type, x.WeaponType ), x => x);
        }

        public WeaponConfig ForWeapon(WeaponType type) =>
            _weapons.TryGetValue(type, out WeaponConfig weaponConfig)
                ? weaponConfig
                : null;

        public EnemyConfig ForEnemy(EnemyType type) =>
            _enemies.TryGetValue(type, out EnemyConfig enemyConfig) 
                ? enemyConfig
                : null;

        public HeroConfig ForHero(HeroType type) =>
            _heroes.TryGetValue(type, out HeroConfig heroConfig) 
                ? heroConfig
                : null;
        
        public PowerUpConfig ForPowerUp((PowerUpType, WeaponType) type) =>
            _powerUps.TryGetValue(type, out PowerUpConfig powerUpConfig) 
                ? powerUpConfig
                : null;

        public PowerUpConfig[] ForPowerUps => _powerUps.Values.ToArray();
    }
}