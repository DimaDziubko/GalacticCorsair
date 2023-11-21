using UnityEngine;

namespace _Game.Enemies.Scripts
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "StaticData/Enemy Config")]
    public class EnemyGeneralConfig : ScriptableObject
    {
        public EnemyConfig[] Configs;
    }
}