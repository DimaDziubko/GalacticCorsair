using System;
using _Game._Hero.Scripts.Factory;
using _Game._Hero.Shield.Scripts;
using _Game._Weapon.Scripts.Factory;
using _Game.Common;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Input;
using _Game.Enemies.Scripts;
using _Game.PowerUp.Scripts;
using UnityEngine;

namespace _Game._Hero.Scripts
{
    public class Hero : GameBehaviour
    {
        public event Action<float, float> HealthChanged;
        public HeroFactory OriginFactory { get; set; }
        
        [SerializeField] private HeroMove _heroMove;
        [SerializeField] private HeroWeapon _heroWeapon;
        [SerializeField] private HeroShield _heroShield;
        [SerializeField] private BoundsCheck _boundsCheck;

        private float _health;
        private float _maxHealth;
        private bool _isDead => _health <= 0;
        public bool IsDead
        {
            get => _isDead;
        }

        public void Construct(
            IInputService inputService,
            float health,
            float shieldCapacity,
            float rippleDuration,
            IWeaponFactory weaponFactory,
            IWorldCameraService cameraService,
            IVfxAudioSourceService audioSourceService,
            AudioClip shieldHitSound,
            HeroMovementConfig movementConfig)
        {
            _health = _maxHealth = health;
            _heroMove.Construct(inputService, movementConfig);
            _heroWeapon.Construct(inputService, weaponFactory, _heroMove);
            _heroShield.Construct(
                shieldCapacity, 
                rippleDuration,
                audioSourceService,
                shieldHitSound);
            _boundsCheck.Construct(cameraService);
        }

        public void SubscribeToShieldCapacityChange(Action<float, float> handler)
        {
            _heroShield.ShieldCapacityChanged += handler;
        }

        public void UnSubscribeToShieldCapacityChange(Action<float, float> handler)
        {
            _heroShield.ShieldCapacityChanged -= handler;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            
            HealthChanged?.Invoke(_health, _maxHealth);
        }

        public override bool GameUpdate()
        {
            if (_isDead)
            {
                _heroWeapon.ClearWeapon();
                Recycle();
                return false;
            }
            _heroMove.GameUpdate();
            _heroWeapon.GameUpdate();
            _heroShield.GameUpdate();
            return true;
        }

        public override void GameLateUpdate()
        {
            if(_isDead) return;
            if(_boundsCheck)
                _boundsCheck.GameLateUpdate();
        }

        public override void Recycle()
        {
            OriginFactory.Reclaim(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Hero collision");
            if (other.collider.TryGetComponent(out Enemy enemy))
            {
                TakeFullDamage();
                enemy.TakeFullDamage();
                return;
            }
            
            if (other.collider.TryGetComponent(out Powerup powerUp))
            {
                Absorb(powerUp);
            }
        }

        private void Absorb(Powerup powerUp)
        {
            switch (powerUp.PowerUpType)
            {
                case PowerUpType.Health:
                    break;
                case PowerUpType.Energy:
                    break;
                case PowerUpType.Weapon:
                    _heroWeapon.Upgrade(powerUp.WeaponType);
                    break;
            }

            powerUp.Absorbed = true;
        }

        private void TakeFullDamage()
        {
            _health = 0;

            HealthChanged?.Invoke(_health, _maxHealth);
        }
    }
}