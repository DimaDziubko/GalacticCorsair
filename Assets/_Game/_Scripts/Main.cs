// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
//
// namespace _Game._Scripts
// {
//     public class Main : MonoBehaviour
//     {  
//         public static Main S;
//         static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
//
//         [Header("Set in Inspector")]
//         public float enemyDefaultPadding = 1.5f;
//         public WeaponDefinition[] weaponDefinitions;
//         public GameObject prefabPowerUp;
//         public WeaponType[] powerUpFrequency = new WeaponType[]
//         {
//             WeaponType.blaster, WeaponType.blaster,
//             WeaponType.spread, WeaponType.shield
//         };
//
//
//         private AudioSource audioSource;
//         private BoundsCheck bndCheck;
//         private float delayExplosion = 1f;
//
//         [Header("Set Dynamically")]
//         public TextMeshProUGUI scoreGT;
//         public List<GameObject> enemies;
//
//         public void ShipDestroyed(Enemy e)
//         {
//             enemies.Remove(e.gameObject);
//
//             if (Random.value <= e.powerUpDropChance)
//             {
//                 int ndx = Random.Range(0, powerUpFrequency.Length);
//                 WeaponType puType = powerUpFrequency[ndx];
//
//                 GameObject go = Instantiate(prefabPowerUp) as GameObject;
//                 PowerUp pu = go.GetComponent<PowerUp>();
//                 pu.SetType(puType);
//                 pu.transform.position = e.transform.position;
//
//             }
//
//             //Add score
//             int score = int.Parse(scoreGT.text);
//             score += e.score;
//             scoreGT.text = score.ToString();
//
//             if(score > HighScore.score)
//             {
//                 HighScore.score = score;
//             }
//         }
//         
//         void Awake()
//         {
//             // S = this;
//             //
//             // bndCheck = GetComponent<BoundsCheck>();
//             //
//             // //Play BGM.
//             // audioSource = GetComponent<AudioSource>();
//             // audioSource.Play();
//             //
//             // Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
//             //
//             // WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
//             // foreach(WeaponDefinition def in weaponDefinitions)
//             // {
//             //     WEAP_DICT[def.type] = def;
//             // }
//             //
//             // GameObject scoreGO = GameObject.Find("Score");
//             // scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
//             // scoreGT.text = "0";
//         }
//
//         public void SpawnEnemy()
//         {
//             // int ndx = Random.Range(0, prefabEnemies.Length);
//             // GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
//             //
//             // float enemyPadding = enemyDefaultPadding;
//             // if(go.GetComponent<BoundsCheck>() != null)
//             // {
//             //     enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
//             // }
//             //
//             // Vector3 pos = Vector3.zero;
//             // float xMin = -bndCheck.camWidth + enemyPadding;
//             // float xMax = bndCheck.camWidth - enemyPadding;
//             // pos.x = Random.Range(xMin, xMax);
//             // pos.y = bndCheck.camHeight + enemyPadding;
//             // go.transform.position = pos;
//             //
//             // enemies.Add(go);
//             //
//             // Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
//         }
//         public static WeaponDefinition GetWeaponDefinition(WeaponType wt)
//         {
//             if (WEAP_DICT.ContainsKey(wt))
//             {
//                 return (WEAP_DICT[wt]);
//             }
//
//             return (new WeaponDefinition());
//         }
//     }
// }
