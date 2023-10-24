using UnityEngine;

namespace _Game.Core
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private AudioSource[] _levelSounds;

        public static SceneContext Instance { get; private set;}

        public AudioSource[] LevelSounds => _levelSounds;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}