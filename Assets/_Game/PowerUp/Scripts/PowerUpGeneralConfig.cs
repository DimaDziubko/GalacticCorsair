using UnityEngine;

namespace _Game.PowerUp.Scripts
{
    [CreateAssetMenu(fileName = "PowerUpConfig", menuName = "StaticData/PowerUp Config")]
    public class PowerUpGeneralConfig : ScriptableObject
    {
        public PowerUpConfig[] Configs;
    }
}