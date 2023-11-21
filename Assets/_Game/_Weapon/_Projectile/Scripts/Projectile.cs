using _Game._Weapon._Projectile.Factory;
using _Game._Weapon.Scripts;
using _Game.Common;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Enemies.Scripts;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;

namespace _Game._Weapon._Projectile.Scripts
{
    [RequireComponent(
        typeof(Rigidbody), 
        typeof(BoundsCheck))]
    public class Projectile : GameBehaviour
    {
        [SerializeField] private BoundsCheck _boundsCheck;

        public ProjectileFactory OriginFactory { get; set; }

        public Rigidbody Rigidbody;
        
        private WeaponType _type;

        private IVfxFactory _vfxFactory;

        private IVfxAudioSourceService _audioSourceService;
        private AudioClip _hitSound;

        private float _damageOnHit;

        private bool _isDestroyed = false;
        
        public void Construct(
            WeaponType type,
            float damageOnHit,
            IWorldCameraService cameraService,
            IVfxFactory vfxFactory,
            IVfxAudioSourceService audioSourceService,
            AudioClip hitSound)
        {
            _type = type;
            _damageOnHit = damageOnHit;
            _vfxFactory = vfxFactory;
            _boundsCheck.Construct(cameraService);
            _audioSourceService = audioSourceService;
            _hitSound = hitSound;
        }
        
        public override bool GameUpdate()
        {
            if (!_boundsCheck.IsOnScreen || _isDestroyed)
            {
                Recycle();
                return false;
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

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out Enemy enemy))
            {

                ShowImpact();
                PlaySound();

                enemy.TakeDamage(_damageOnHit);

                _isDestroyed = true;
            }
        }

        private void PlaySound()
        {
            _audioSourceService.PlayOneShot(_hitSound);
        }

        private void ShowImpact()
        {
            var impact = _vfxFactory.GetImpact(_type);
            Vector3 impactPosition = transform.position - Vector3.forward;
            impact.transform.position = impactPosition;
        }
    }
}