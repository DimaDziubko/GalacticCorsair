using System.Collections.Generic;
using System.Linq;
using _Game._Hero.Scripts;
using _Game._Weapon.Scripts;
using _Game.Core.Services._Game.StaticData;
using _Game.Enemies.Scripts;
using UnityEngine;

namespace _Game.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WEAPON_STATIC_DATA_PATH = "Weapons";
        private const string ENEMY_STATIC_DATA_PATH = "Enemies";
        private const string HERO_STATIC_DATA_PATH = "Heroes";
        
        private Dictionary<WeaponType, WeaponConfig> _weapons;
        private Dictionary<EnemyType, EnemyConfig> _enemies;
        private Dictionary<HeroType, HeroConfig> _heroes;

        public void LoadHeroes()
        {
            _heroes = Resources.LoadAll<HeroConfig>(HERO_STATIC_DATA_PATH)
                .ToDictionary(x=>x.Type, x => x);
        }
        
        public void LoadWeapons()
        {
            _weapons = Resources.LoadAll<WeaponConfig>(WEAPON_STATIC_DATA_PATH)
                .ToDictionary(x=>x.Type, x => x);
        }
        
        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyConfig>(ENEMY_STATIC_DATA_PATH)
                .ToDictionary(x=>x.Type, x => x);
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
    }
}