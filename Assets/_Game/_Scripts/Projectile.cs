// using UnityEngine;
//
// namespace _Game._Scripts
// {
//     public class Projectile : MonoBehaviour
//     {
//         private BoundsCheck bndCheck;
//         private Renderer rend;
//
//         [Header("Set Dynamically")]
//         public Rigidbody rigid;
//         [SerializeField]
//         private WeaponType _type;
//
//         [Header("Set in Inspector")]
//         float x0;
//         float birthTime;
//
//         public WeaponType type      
//         {
//             get
//             {
//                 return (_type);
//             }
//             set
//             {
//                 SetType(value);
//             }
//         }
//
//
//         void Awake()
//         {
//             bndCheck = GetComponent<BoundsCheck>();
//             rend = GetComponent<Renderer>();
//             rigid = GetComponent<Rigidbody>();
//         }
//
//         private void Start()
//         {
//             x0 = pos.x;
//             birthTime = Time.time;
//         }
//
//         void Update()
//         {
//             if (bndCheck.offUp)
//             {
//                 Destroy(gameObject);
//             }
//
//             switch (type)
//             {
//                 case WeaponType.phaser:
//                     SinusoidMove();
//                     break;
//             }
//         }
//
//         private void SinusoidMove()
//         {
//             float waveWigth = 3;
//             float waveFrequency = 0.25f;
//             Vector3 tempPos = pos;
//
//             float age = Time.time - birthTime;
//             float theta = Mathf.PI * 2 * age / waveFrequency;
//
//             float sin = Mathf.Sin(theta);
//             tempPos.x = x0 + waveWigth * sin;
//             pos = tempPos;
//         }
//
//         public void SetType(WeaponType eType)
//         {
//             _type = eType;
//             WeaponDefinition def = Main.GetWeaponDefinition(_type);
//             rend.material.color = def.projectileColor;
//
//         }
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
//     }
// }
