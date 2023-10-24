// using UnityEngine;
//
// namespace _Game._Scripts
// {
//     public class Laser : Projectile
//     {
//         [Header("Set Dynamically")]
//         [SerializeField]
//         private LineRenderer _lineRenderer;
//         public Vector3 firstPosition { get; set; }
//     
//
//         // Start is called before the first frame update
//         void Start()
//         {
//             _lineRenderer = this.gameObject.GetComponent<LineRenderer>();
//         }
//
//         // Update is called once per frame
//         void Update()
//         {
//             Ray ray = new Ray(transform.position, Vector3.up);
//
//             Debug.DrawRay(transform.position, Vector3.up * 100f, Color.yellow);
//             RaycastHit hit;
//             if (Physics.Raycast(ray, out hit) && hit.rigidbody.gameObject.tag == "Enemy")
//             {
//                 _lineRenderer.enabled = true;
//                 firstPosition = transform.position;
//                 var secondPosition = hit.point;
//                 _lineRenderer.SetPosition(0, firstPosition);
//                 _lineRenderer.SetPosition(1, secondPosition);
//             }
//             else
//             {
//                 _lineRenderer.enabled = true;
//                 firstPosition = transform.position;
//                 var secondPosition = transform.position + Vector3.up * 100f;
//                 _lineRenderer.SetPosition(0, firstPosition);
//                 _lineRenderer.SetPosition(1, secondPosition);
//             }
//
//             Destroy(this.gameObject, 4);
//         }
//     }
// }
