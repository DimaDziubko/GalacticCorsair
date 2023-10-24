using UnityEngine;

namespace _Game._Hero.Scripts
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "StaticData/Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        public HeroType Type = HeroType.None;
        
        public Hero Prefab;
        
        [Range(15f, 50f)]
        public float Speed = 30f;
            
        [Range(-90f, 90f)]
        public float RollMult = -45f;
            
        [Range(0f, 60f)]
        public float PitchMult = 30f;
        
        [Range(0f, 10f)]
        public float Health = 5f;

        [Range(0f, 5f)]
        public float ShieldCapacity = 4f;
        
        [Range(0f, 2f)]
        public float RippleDuration = 1f;

        public AudioClip ShieldHitSound;

    }
}