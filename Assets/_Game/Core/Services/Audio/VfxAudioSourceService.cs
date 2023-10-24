using UnityEngine;

namespace _Game.Core.Services.Audio
{
    public class VfxAudioSourceService : IVfxAudioSourceService
    {
        private AudioSource[] _audioSources => SceneContext.Instance.LevelSounds;
        
        private int _freeSource;

        public void PlayOneShot(AudioClip audioClip)
        {
            if (_freeSource >= _audioSources.Length - 1)
            {
                _freeSource = 0;
            }
            
            //_audioSources[_freeSource].PlayOneShot(audioClip);
            _audioSources[_freeSource].PlayOneShot(audioClip);

            //Debug.Log($"Playing AudioSource { _freeSource}");
            
            _freeSource += 1;
        }

        // public void PlayOneShot(AudioClip audioClip)
        // {
        //     if (_audioSource.isPlaying && audioClip == _audioSource.clip)
        //     {
        //         _audioSource.PlayDelayed(0.01f);
        //     }
        //     _audioSource.clip = audioClip;
        //     _audioSource.Play();
        // }
    }
}