// using UnityEngine;
//
// namespace _Game._Scripts
// {
//     public class Rocket : MonoBehaviour
//     {
//         [Header("Set in Inspector")]
//         public float maxDistanceDelta = 0.1f;
//
//         public float rocketSpeed { get; set; }
//
//         private Rigidbody _rigid;
//         private int targetIndex;
//
//         private void Awake()
//         {
//             _rigid = gameObject.GetComponent<Rigidbody>();
//             targetIndex = Random.Range(0, Main.S.enemies.Count);
//         }
//
//         // Update is called once per frame
//         void Update()
//         {
//             if(Main.S.enemies.Count > 0)
//             {    
//                 if(targetIndex > Main.S.enemies.Count - 1)
//                 {
//                     targetIndex = 0;
//                 }
//                 _rigid.velocity = Vector3.zero;
//                 Vector3 target = Main.S.enemies[targetIndex].transform.position;
//                 Vector3 toTarget = target - transform.position;
//                 Quaternion rotation = Quaternion.LookRotation(toTarget);
//
//                 transform.rotation = rotation;
//                 transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
//             }
//             else
//             {
//                 _rigid.velocity = transform.forward * rocketSpeed;
//             }
//         }
//     }
// }
