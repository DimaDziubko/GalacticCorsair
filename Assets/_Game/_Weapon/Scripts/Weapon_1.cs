// using UnityEngine;
//
// namespace _Game._Scripts
// {
//     public enum WeaponType
//     { 
//         none,
//         blaster,
//         spread,
//         phaser,
//         missile,
//         laser,
//         shield,
//
//         enemyBlaster
//     }
//
//     [System.Serializable]
//     public class WeaponDefinition
//     {
//         public WeaponType type = WeaponType.none;
//         public string letter;
//
//         public Color color = Color.white;
//         public GameObject weaponMesh;
//         public GameObject projectilePrefab;
//         public AudioClip weaponTypeSound;
//         public Color projectileColor = Color.white;
//         public float damageOnHit = 0;
//         public float continuousDamage = 0;
//
//         public float delayBetweenShots = 0;
//         public float velocity = 20;
//
//     }
//
//     public class Weapon : MonoBehaviour
//     {
//         static public Transform PROJECTILE_ANCHOR;
//
//         [Header("Set Dynamically")]
//         [SerializeField]
//         private WeaponType _type = WeaponType.none;
//         private AudioSource audioSource;
//         public WeaponDefinition def;
//         public GameObject collar;
//         public float lastShotTime;
//
//         void Start()
//         {
//             collar = transform.Find("Collar").gameObject;
//             SetType(_type);
//
//             if(PROJECTILE_ANCHOR == null)
//             {
//                 GameObject go = new GameObject("_ProjectileAnchor");
//                 PROJECTILE_ANCHOR = go.transform;
//             }
//
//             GameObject rootGO = transform.root.gameObject;
//             if (rootGO.GetComponent<Hero>() != null)
//             {
//                 rootGO.GetComponent<Hero>().fireDelegate += Fire;
//             }
//
//             if (rootGO.GetComponent<Enemy_4>() != null)
//             {
//                 rootGO.GetComponent<Enemy_4>().fireDelegate += Fire;
//             }
//
//             audioSource = GetComponent<AudioSource>();
//         }
//         public WeaponType type
//         {
//             get { return (_type); }
//             set { SetType(value); }
//         }
//
//         public void SetType(WeaponType wt)
//         {
//             _type = wt;
//             if(type == WeaponType.none)
//             {
//                 this.gameObject.SetActive(false);
//                 return;
//             }
//             else
//             {
//                 this.gameObject.SetActive(true);
//             }
//             def = Main.GetWeaponDefinition(_type);
//
//             GameObject go = Instantiate<GameObject>(def.weaponMesh);
//             go.transform.position = transform.position;
//             go.transform.SetParent(transform, true);
//
//             lastShotTime = 0;
//         }
//
//         public void Fire()
//         {
//             if (!gameObject.activeInHierarchy) return;
//
//             if(Time.time - lastShotTime < def.delayBetweenShots)
//             {
//                 return;
//             }
//
//             Projectile p;
//             Vector3 vel = Vector3.up * def.velocity;
//             if(transform.up.y < 0)
//             {
//                 vel.y = -vel.y;
//             }
//             switch (type)
//             {
//                 case WeaponType.blaster:
//                     p = MakeProjectile();
//                     audioSource.PlayOneShot(def.weaponTypeSound);
//                     p.rigid.velocity = vel;
//                     break;
//
//                 case WeaponType.spread:
//                     p = MakeProjectile();
//                     audioSource.PlayOneShot(def.weaponTypeSound);
//                     p.rigid.velocity = vel;
//                     p = MakeProjectile();
//                     p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
//                     p.rigid.velocity = p.transform.rotation * vel;
//                     p = MakeProjectile();
//                     p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
//                     p.rigid.velocity = p.transform.rotation * vel;
//                     break;
//
//                 case WeaponType.phaser:
//                     p = MakeProjectile();
//                     audioSource.PlayOneShot(def.weaponTypeSound);
//                     p.rigid.velocity = vel;
//                     break;
//
//                 case WeaponType.missile:
//                     p = MakeProjectile();
//                     audioSource.PlayOneShot(def.weaponTypeSound);
//                     p.GetComponent<Rocket>().rocketSpeed = def.velocity;
//                     break;
//
//                 case WeaponType.laser:
//                     audioSource.PlayOneShot(def.weaponTypeSound);
//                     lastShotTime = Time.time;
//                     p = MakeProjectile();
//                     p.transform.SetParent(this.transform, true);
//                     p.GetComponent<Laser>().firstPosition = transform.position;
//                     break;
//
//                 case WeaponType.enemyBlaster:
//                     p = MakeProjectile();
//                     audioSource.PlayOneShot(def.weaponTypeSound);
//                     p.rigid.velocity = vel;
//                     break;
//
//             }
//         }
//
//         public Projectile MakeProjectile()
//         {
//             GameObject go = Instantiate<GameObject>(def.projectilePrefab);
//             if(transform.parent.gameObject.tag == "Hero")
//             {
//                 go.tag = "ProjectileHero";
//                 go.layer = LayerMask.NameToLayer("ProjectileHero");
//             }
//             else
//             {
//                 go.tag = "ProjectileEnemy";
//                 go.layer = LayerMask.NameToLayer("ProjectileEnemy");
//             }
//
//             go.transform.position = collar.transform.position;
//             if(go.tag == "ProjectileHero" && _type != WeaponType.laser)
//             {
//                 go.transform.SetParent(PROJECTILE_ANCHOR, true);
//             }    
//             Projectile p = go.GetComponent<Projectile>();
//             p.type = type;
//             lastShotTime = Time.time;
//             return (p);
//         }
//     }
// }