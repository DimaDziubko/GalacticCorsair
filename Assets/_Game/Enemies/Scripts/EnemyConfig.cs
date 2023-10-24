using _Game.Common;
using _Game.Vfx.Scripts;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "StaticData/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public EnemyType Type = EnemyType.Ufo;

        public Enemy EnemyPrefab;
        
        [FloatRangeSlider(5f, 20f)]
        public FloatRange Speed = new FloatRange(10f);
        
        [FloatRangeSlider(0.1f, 0.7f)]
        public FloatRange FireRate = new FloatRange(0.3f);
        
        [FloatRangeSlider(1f, 20f)]
        public FloatRange Health = new FloatRange(2f);
        
        public int Score = 100;
        
        public float ShowDamageDuration = 0.1f;
        
        [FloatRangeSlider(0f, 1f)]
        public FloatRange PowerUpDropChance = new FloatRange(0.5f);
        
        //VFX
        public Explosion ExplosionPrefab;
        public float ExplosionDuration;

        //Sounds
        public AudioClip ImpactSound;
    }
}