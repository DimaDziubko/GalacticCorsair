using UnityEngine;

namespace _Game.Core.Services.Audio
{
    public interface IVfxAudioSourceService : IService
    {
        void PlayOneShot(AudioClip audioClip);
    }
}