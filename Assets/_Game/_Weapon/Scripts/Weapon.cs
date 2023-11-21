using _Game._Weapon._Projectile.Factory;
using _Game._Weapon._Projectile.Scripts;
using _Game._Weapon.Scripts.Factory;
using _Game.Core.Services.Audio;
using _Game.Vfx.Scripts;
using _Game.Vfx.Scripts.Factory;
using UnityEngine;

namespace _Game._Weapon.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _collarTransform;
        [SerializeField] private GameObject _weaponModel;

        public bool ModelVisibility
        {
            set => _weaponModel.SetActive(value);
        }
        
        private IVfxFactory _vfxFactory;
        
        private AudioClip _shotSound;
        
        public IWeaponFactory OriginFactory { get; set; }

        private IProjectileFactory _projectileFactory;
        
        private IVfxAudioSourceService _audioSourceService;

        private WeaponType _type;

        private float _delayBetweenShots;
        private float _velocity;
        
        private float _lastShotTime;

        public void Construct(
            IProjectileFactory projectileFactory,
            WeaponConfig weaponConfig,
            IVfxAudioSourceService audioSourceService,
            IVfxFactory vfxFactory)
        {
            _projectileFactory = projectileFactory;
            _type = weaponConfig.Type;
            _delayBetweenShots = weaponConfig.DelayBetweenShots;
            _velocity = weaponConfig.Velocity;
            _shotSound = weaponConfig.ShotSound;
            _audioSourceService = audioSourceService;
            _vfxFactory = vfxFactory;
        }

        public void Fire(Vector3 direction)
        {
            if(Time.time - _lastShotTime < _delayBetweenShots)
                return;

            Projectile projectile;
             Vector3 velocity = direction * _velocity;

             switch (_type)
             {
                 case WeaponType.Blaster:
                     projectile = MakeProjectile();
                     projectile.Rigidbody.velocity = velocity;
                     break;
             }

             ShowMuzzleFlash();
             
             PlaySound();
             
             _lastShotTime = Time.time;
        }

        private void PlaySound()
        {
            if (_audioSourceService != null && _shotSound != null)
            {
                _audioSourceService.PlayOneShot(_shotSound);
            }
        }

        private void ShowMuzzleFlash()
        {
           MuzzleFlash muzzleFlash = _vfxFactory.GetMuzzleFlash(WeaponType.Blaster);
           muzzleFlash.Position = _collarTransform.position;
           muzzleFlash.Rotation = _collarTransform.rotation;
        }

        private Projectile MakeProjectile()
        {
            Projectile projectile = _projectileFactory.Get(_type);
            projectile.transform.position = _collarTransform.position;
            return projectile;
        }
        
    }
}