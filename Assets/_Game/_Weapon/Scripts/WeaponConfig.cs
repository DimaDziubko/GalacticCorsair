using _Game._Weapon._Projectile.Scripts;
using _Game.Vfx.Scripts;
using UnityEngine;

namespace _Game._Weapon.Scripts
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "StaticData / Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
         public WeaponType Type = WeaponType.None;
         
         public Weapon WeaponPrefab;

         public Projectile ProjectilePrefab;

         public Impact ImpactPrefab;
         
         public float ImpactDuration;
         
         public MuzzleFlash MuzzleFlash;

         public float MuzzleDuration;
         
         public AudioClip ShotSound;
         
         public AudioClip HitSound;
         
         public string Letter;

         public Color color = Color.white;

         [Range(0f, 100f)]
         public float DamageOnHit = 0;
         
         public float ContinuousDamage = 0;

         [Range(0f, 10f)]
         public float DelayBetweenShots = 0;
         
         [Range(0f, 50f)]
         public float Velocity = 20;

    }
}