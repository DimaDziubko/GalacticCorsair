using _Game.Common;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.Enemies.Scripts.Factory;
using _Game.PowerUp.Scripts;
using _Game.PowerUp.Scripts.Factory;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    public class Enemy : GameBehaviour
    {
        public EnemyFactory OriginFactory { get; set; }

        [SerializeField] private EnemyMove _move;
        [SerializeField] private BoundsCheck _boundsCheck;
        
        private EnemyType _type;

        private IVfxFactory _vfxFactory;
        private IVfxAudioSourceService _audioSourceService;
        private IPowerUpFactory _powerUpFactory;

        private IRandomService _randomService;

        private AudioClip _impactSound;

        private float _powerUpDropChance;


        private float _health;
        private bool IsDead => _health <= 0;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public float Radius
        {
            get => _boundsCheck.Radius;
        }
        
        public void Construct(
            EnemyType type,
            EnemyMovementConfig movementConfig,
            float health,
            AudioClip impactSound,
            float powerUpDropChance,
            IWorldCameraService cameraService,
            IVfxAudioSourceService audioSourceService,
            IVfxFactory vfxFactory,
            IRandomService randomService,
            IPowerUpFactory powerUpFactory)
        {
            _type = type;
            _move.Initialize(movementConfig, cameraService, randomService);
            _health = health;
            _impactSound = impactSound;
            _boundsCheck.Construct(cameraService);
            _audioSourceService = audioSourceService;
            _vfxFactory = vfxFactory;
            _powerUpDropChance = powerUpDropChance;
            _randomService = randomService;
            _powerUpFactory = powerUpFactory;
        }

        public override bool GameUpdate()
        {
            if (IsDead)
            {
                ShowImpact();
                PlaySound();
                DropPowerUp();
                Recycle();
                return false;
            }
            if (!_boundsCheck.IsOnScreen)
            {
                Recycle();
                return false;
            }
            _move.GameUpdate();
            return true;
        }

        public override void GameLateUpdate()
        {
            if (IsDead)
            {
                return;
            }
            if(_boundsCheck)
                _boundsCheck.GameLateUpdate();
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }

        public void TakeFullDamage()
        {
            _health = 0;
        }
        
        public override void Recycle()
        {
            OriginFactory.Reclaim(this);
        }

        private void ShowImpact() 
        {  
            var impact = _vfxFactory.GetExplosion(_type);
            impact.transform.position = transform.position;
        }
        
        private void PlaySound()
        {
            _audioSourceService.PlayOneShot(_impactSound);
        }

        private void DropPowerUp()
        {
            float value = _randomService.GetValue();
            if (value <= _powerUpDropChance)
            {
                Powerup powerUp = _powerUpFactory.Get();
                powerUp.Position = transform.position;
            }
        }
    }
}