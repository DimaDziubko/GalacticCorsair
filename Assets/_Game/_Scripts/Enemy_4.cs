// using System.Collections.Generic;
// using UnityEngine;
//
// namespace _Game._Scripts
// {
//     [System.Serializable]
//     public class Part
//     {
//         public string name;
//         public float health;
//         public string[] protectedBy;
//
//         [HideInInspector]
//         public GameObject go;
//
//         [HideInInspector]
//         public Material mat;
//
//     }
//
//     public class Enemy_4 : Enemy
//     {
//         [Header("Set in Inspector")]
//         public Part[] parts;
//
//         private Vector3 p0, p1;
//         private float timeStart;
//         private float duration = 4;
//
//         // Start is called before the first frame update
//         void Start()
//         {
//             p0 = p1 = pos;
//             InitMovement();
//
//             Transform t;
//             foreach (Part prt in parts)
//             {
//                 t = transform.Find(prt.name);
//                 if (t != null)
//                 {
//                     prt.go = t.gameObject;
//                     prt.mat = prt.go.GetComponent<Renderer>().material;
//                 }
//             }
//         }
//
//         void InitMovement()
//         {
//             p0 = p1;
//             float widMidRad = bndCheck.camWidth - bndCheck.radius;
//             float hgtMidRad = bndCheck.camHeight - bndCheck.radius;
//             p1.x = Random.Range(-widMidRad, widMidRad);
//             p1.y = Random.Range(-hgtMidRad, hgtMidRad);
//
//             timeStart = Time.time;
//         }
//
//         public override void Move()
//         {
//             float u = (Time.time - timeStart) / duration;
//
//             if (u >= 1)
//             {
//                 InitMovement();
//                 u = 0;
//             }
//
//             u = 1 - Mathf.Pow(1 - u, 2);
//             pos = (1 - u) * p0 + u * p1;
//         }
//
//         Part FindPart(string n)
//         {
//             foreach (Part prt in parts)
//             {
//                 if (prt.name == n)
//                 {
//                     return (prt);
//                 }        
//             }
//             return (null);
//         }
//
//         Part FindPart(GameObject go)
//         {
//             foreach (Part prt in parts)
//             {
//                 if(prt.go == go)
//                 {
//                     return (prt);
//                 }
//             }
//             return (null);
//         }
//
//         bool Destroyed(GameObject go)
//         {
//             return (Destroyed(FindPart(go)));
//         }
//
//         bool Destroyed(string n)
//         {
//             return (Destroyed(FindPart(n)));
//         }
//
//         bool Destroyed(Part prt)
//         {
//             if(prt == null)
//             {
//                 return (true);
//             }
//             return (prt.health <= 0);
//         }
//
//         void ShowLocalizedDamage(Material m)
//         {
//             m.color = Color.red;
//             damageDoneTime = Time.time + showDamageDuration;
//             showingDamage = true;
//         }
//
//         void OnCollisionEnter(Collision coll)
//         {
//             GameObject other = coll.gameObject;
//             switch (other.tag)
//             {
//                 case "ProjectileHero":
//                     Projectile p = other.GetComponent<Projectile>();
//                     if (!bndCheck.isOnScreen)
//                     {
//                         Destroy(other);
//                         break;
//                     }
//
//                     GameObject goHit = coll.contacts[0].thisCollider.gameObject;
//                     Part prtHit = FindPart(goHit);
//                     if(prtHit == null)
//                     {
//                         goHit = coll.contacts[0].otherCollider.gameObject;
//                         prtHit = FindPart(goHit);
//                     }
//                     if(prtHit.protectedBy != null)
//                     {
//                         foreach (string s in prtHit.protectedBy)
//                         {
//                             if (!Destroyed(s))
//                             {
//                                 Destroy(other);
//                                 return;
//                             }
//                         }
//                     }
//                     prtHit.health -= Main.GetWeaponDefinition(p.type).damageOnHit;
//
//                     ShowLocalizedDamage(prtHit.mat);
//                     if (prtHit.health <= 0)
//                     {
//                         prtHit.go.SetActive(false);
//                     }
//                     bool allDestroyed = true;
//                     foreach (Part prt in parts)
//                     {
//                         if (!Destroyed(prt))
//                         {
//                             allDestroyed = false;
//                             break;
//                         }
//                     }
//                     if (allDestroyed)
//                     {
//                         Main.S.ShipDestroyed(this);
//                         Destroy(this.gameObject);
//                         Main.S.enemies.Remove(gameObject);
//                     }
//                     Destroy(other);
//                     break;
//             }
//
//         }
//
//         private void OnTriggerStay(Collider coll)
//         {
//             GameObject other = coll.gameObject;
//             switch (other.tag)
//             {
//                 case "ProjectileHero":
//                     Projectile p = other.GetComponent<Projectile>();
//
//                     List<string> partNames = new List<string> { "enemy4_shield_Left", "enemy4_shield_Right", "enemy4_shield_Front", "enemy4_body" };
//
//                     Part prtHit = FindPart(partNames[0]);
//                     if (prtHit.go.active == false)
//                     {
//                         prtHit = FindPart(partNames[1]);
//                     }
//                     if (prtHit.go.active == false)
//                     {
//                         prtHit = FindPart(partNames[2]);
//                     }
//                     if (prtHit.go.active == false)
//                     {
//                         prtHit = FindPart(partNames[3]);
//                     }
//
//                     //if (prtHit == null || prtHit.go.active == false)
//                     //{
//                     //    prtHit = FindPart(partNames[0]);
//                     //}
//                     if (prtHit.protectedBy != null)
//                     {
//                         foreach (string s in prtHit.protectedBy)
//                         {
//                             if (!Destroyed(s))
//                             {
//                                 return;
//                             }
//                         }
//                     }
//
//                     prtHit.health -= Main.GetWeaponDefinition(p.type).continuousDamage * Time.deltaTime;
//
//                     ShowLocalizedDamage(prtHit.mat);
//                     if (prtHit.health <= 0)
//                     {
//                         prtHit.go.SetActive(false);
//                         partNames.RemoveAt(0);
//                     }
//                     bool allDestroyed = true;
//                     foreach (Part prt in parts)
//                     {
//                         if (!Destroyed(prt))
//                         {
//                             allDestroyed = false;
//                             break;
//                         }
//                     }
//                     if (allDestroyed)
//                     {
//                         Main.S.ShipDestroyed(this);
//                         Destroy(this.gameObject);
//                         Main.S.enemies.Remove(gameObject);
//                     }
//                     break;
//             }
//         }
//
//         public override void EnemyFire()
//         {
//             Vector3 rayStart = new Vector3(0, -4, 0);
//
//             Ray ray = new Ray(transform.position + rayStart, -Vector3.up);
//             Debug.DrawRay(transform.position + rayStart, -Vector3.up * 100f, Color.yellow);
//
//             RaycastHit hit;
//
//             if(Physics.Raycast(ray, out hit) && hit.rigidbody.gameObject.tag == "Hero")
//             {
//                 fireDelegate();
//             }
//         }
//
//     }
// }