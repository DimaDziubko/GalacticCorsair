// using UnityEngine;
//
// namespace _Game._Scripts
// {
//     public class Enemy : MonoBehaviour
//     {
//         [Header("Set in Inspector")]
//         public float speed = 10f;
//         public float fireRate = 0.3f;
//         public float health = 10;
//         public int score = 100;
//         public float showDamageDuration = 0.1f;
//         public float powerUpDropChance = 1f;
//
//         [Header("Set Dynamically")]
//         public Color[] originalColors;
//         public Material[] materials;
//         public bool showingDamage = false;
//         public float damageDoneTime;
//         public bool notifiedOfDestruction = false;
//
//         protected BoundsCheck bndCheck;
//
//         public delegate void WeaponFireDelegate();
//         public WeaponFireDelegate fireDelegate;
//
//         public Vector3 pos
//         {
//             get
//             {
//                 return (this.transform.position);
//             }
//             set
//             {
//                 this.transform.position = value;
//             }
//         }
//
//         void Awake()
//         {
//             bndCheck = GetComponent<BoundsCheck>();
//             materials = Utils.GetAllMaterials(gameObject);
//             originalColors = new Color[materials.Length];
//
//             for (int i = 0; i < materials.Length; i++)
//             {
//                 originalColors[i] = materials[i].color;
//             }
//         }
//
//         void Update()
//         {
//             Move();
//
//             if(showingDamage && Time.time > damageDoneTime)
//             {
//                 UnShowDamage();
//             }
//
//             if(bndCheck != null && bndCheck.offDown)
//             {
//                 if(pos.y < bndCheck.camHeight - bndCheck.radius)
//                 {
//                     Destroy(gameObject);
//                     Main.S.enemies.Remove(gameObject);
//                 }
//             }
//
//             //if (health <= 0)
//             //{
//             //    Destroy(gameObject);
//             //    Main.S.enemies.Remove(gameObject);
//             //    if (!notifiedOfDestruction)
//             //    {
//             //        Main.S.ShipDestroyed(this);
//             //    }
//             //    notifiedOfDestruction = true;
//             //}
//
//             EnemyFire();
//         }
//
//         public virtual void EnemyFire()
//         {
//         
//         }
//
//         public virtual void Move()
//         {
//             Vector3 tempPos = pos;
//             tempPos.y -= speed * Time.deltaTime;
//             pos = tempPos;
//         }
//
//         void OnCollisionEnter(Collision coll)
//         {
//             GameObject otherGO = coll.gameObject;
//
//             switch (otherGO.tag)
//             {
//                 case "ProjectileHero":
//
//                     Projectile p = otherGO.GetComponent<Projectile>();
//                     if (!bndCheck.isOnScreen)
//                     {
//                         Destroy(otherGO);
//                         break;
//                     }
//
//                     ShowDamage();
//
//                     health -= Main.GetWeaponDefinition(p.type).damageOnHit;
//                     if (health <= 0)
//                     {
//                         if (!notifiedOfDestruction)
//                         {
//                             Main.S.ShipDestroyed(this);
//                         }
//                         notifiedOfDestruction = true;
//                         Destroy(this.gameObject);
//
//                     }
//                     Destroy(otherGO);
//                     break;
//
//                 default:
//                     print("Enemy hit by non - ProjectileHero: " + otherGO.name);
//                     break;      
//             }
//         }
//
//         private void OnTriggerStay(Collider coll)
//         {
//             print("laser hit");
//
//             GameObject otherGO = coll.gameObject;
//
//             switch (otherGO.tag)
//             {
//                 case "ProjectileHero":
//
//                     Projectile p = otherGO.GetComponent<Projectile>();
//
//                     ShowDamage();
//
//                     health -= Main.GetWeaponDefinition(p.type).continuousDamage * Time.deltaTime;
//                     if (health <= 0)
//                     {
//                         if (!notifiedOfDestruction)
//                         {
//                             Main.S.ShipDestroyed(this);
//                         }
//                         notifiedOfDestruction = true;
//                         Destroy(this.gameObject);
//
//                     }
//                     //Destroy(otherGO);
//                     break;
//
//                 default:
//                     print("Enemy hit by non - ProjectileHero: " + otherGO.name);
//                     break;
//             }
//         }
//
//         //public virtual void LaserDamage(Collider coll)
//         //{
//         //    health -= Main.GetWeaponDefinition(WeaponType.laser).continuousDamage * Time.deltaTime;
//         //    ShowDamage();
//         //}
//
//         public void ShowDamage()
//         {
//             foreach(Material m in materials)
//             {
//                 m.color = Color.red;
//             }
//             showingDamage = true;
//             damageDoneTime = Time.time + showDamageDuration;
//         }
//
//         void UnShowDamage()
//         {
//             for (int i = 0; i < materials.Length; i++)
//             {
//                 materials[i].color = originalColors[i];
//             }
//             showingDamage = false;
//         }
//     }
// }
