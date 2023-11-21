using _Game._Weapon.Scripts;
using _Game.Common;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using _Game.PowerUp.Scripts.Factory;
using UnityEngine;

namespace _Game.PowerUp.Scripts
{
    public class Powerup : GameBehaviour
    {
        [SerializeField] private BoundsCheck _boundsCheck;
        [SerializeField] private Rigidbody _rigidbody;
        
        public PowerUpType PowerUpType => _powerUpType;
        public WeaponType WeaponType => _weaponType;

        private IRandomService _randomService;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public bool Absorbed
        {
            get => _absorbed;
            set => _absorbed = value;
        }

        public PowerUpFactory OriginFactory { get; set; }

        private PowerUpType _powerUpType;
        private WeaponType _weaponType;
        private bool _absorbed;

        private float _lifeTime;
        private float _fadeTime;
        private float _birthTime;

        private float _driftMin;
        private float _driftMax;

        public void Construct (
            PowerUpType powerUpType, 
            WeaponType weaponType,
            IWorldCameraService cameraService,
            IRandomService randomService,
            float lifeTime,
            float driftMin,
            float driftMax)
        {
            _powerUpType = powerUpType;
            _weaponType = weaponType;
            _boundsCheck.Construct(cameraService);
            _randomService = randomService;
            Absorbed = false;
            _lifeTime = lifeTime;
            _driftMax = driftMax;
            _driftMin = driftMin;
            
            _birthTime = Time.time;
            
             SetupMove();
        }

        private void SetupMove()
        {
            Vector3 vel = _randomService.OnUnitSphere();
            vel.z = 0;
            vel.Normalize();
            vel *= _randomService.Next(_driftMin, _driftMax);
            _rigidbody.velocity = vel;
        }

        public override bool GameUpdate()
        {
            if (!_boundsCheck.IsOnScreen || Absorbed)
            {
                Recycle();
                return false;
            }

            float u = (Time.time - (_birthTime + _lifeTime)) / _fadeTime;
            
            if (u >= 1)
            {
                Recycle();
                return false;
            }
            if (u > 0)
            {
                // Color c = cubeRend.material.color;
                // c.a = 1f - u;
                // cubeRend.material.color = c;
                // c = letter.color;
                // c.a = 1f - (u * 0.5f);
                // letter.color = c;
            }
            
            return true;
        }

        public override void GameLateUpdate()
        {
            if (_boundsCheck)
            {
                _boundsCheck.GameLateUpdate();
            }
        }

        public override void Recycle()
        {
            OriginFactory.Reclaim(this);
        }
    }
}
