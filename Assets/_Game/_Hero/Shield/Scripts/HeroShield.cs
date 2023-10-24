using System;
using System.Collections;
using _Game.Core.Services.Audio;
using _Game.Enemies.Scripts;
using UnityEngine;

namespace _Game._Hero.Shield.Scripts
{
    public class HeroShield : MonoBehaviour
    {
        public event Action<float, float> ShieldCapacityChanged;

        [SerializeField] private ShieldRipples _ripples;

        private IVfxAudioSourceService _audioSourceService;
        private AudioClip _shieldHitSound;
        
        private float _shieldCapacity;
        private float _maxShieldCapacity;
        private float _rippleDuration;
        private Vector3 _rippleOffset;
        public void Construct(
            float shieldCapacity, 
            float rippleDuration,
            IVfxAudioSourceService audioSourceService,
            AudioClip shieldHitSound)
        {
            _shieldCapacity = _maxShieldCapacity = shieldCapacity;
            _rippleDuration = rippleDuration;
            _ripples.HideRipple();
            _audioSourceService = audioSourceService;
            _shieldHitSound = shieldHitSound;
        }

        public void GameUpdate()
        {
            if (_shieldCapacity <= 0)
            {
                Deactivate();
            }

            _ripples.Adjust(transform.position + _rippleOffset);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Shield collision");
            if (other.collider.TryGetComponent(out Enemy enemy))
            {
                TakeDamage();
                PlayHitSound();
                
                enemy.TakeFullDamage();
                _rippleOffset = other.contacts[0].point - transform.position;
                StartCoroutine(ShowAndHideRipple());
            }
        }

        private void PlayHitSound()
        {
            _audioSourceService.PlayOneShot(_shieldHitSound);
        }

        private IEnumerator ShowAndHideRipple()
        {
            _ripples.ShowRipple();
            yield return new WaitForSeconds(_rippleDuration);
            _ripples.HideRipple();
        }

        private void TakeDamage()
        {
            _shieldCapacity -= 1;
            
            ShieldCapacityChanged?.Invoke(_shieldCapacity, _maxShieldCapacity);
        }

        public void IncreaseCapacity(float value)
        {
            _shieldCapacity += value;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}