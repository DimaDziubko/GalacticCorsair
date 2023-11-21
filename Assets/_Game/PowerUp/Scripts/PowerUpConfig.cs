using System;
using _Game._Weapon.Scripts;
using UnityEngine.Serialization;

namespace _Game.PowerUp.Scripts
{
    [Serializable]
    public class PowerUpConfig
    {
        public PowerUpType Type = PowerUpType.Health;

        public WeaponType WeaponType = WeaponType.None;

        public Powerup PowerupPrefab;

        public int PowerUpFrequency;
        public float LifeTime = 6f;
        public float DriftMin = 0.25f;
        public float DriftMax = 2.0f;
            
    }
}