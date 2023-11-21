using UnityEngine;

namespace _Game._Hero.Scripts
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "StaticData/Hero Config")]
    public class HeroGeneralConfig : ScriptableObject
    {
        public HeroConfig[] Configs;
    }
}