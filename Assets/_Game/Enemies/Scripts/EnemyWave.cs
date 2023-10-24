using System;
using _Game.Levels.Level_1.Scripts;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Core/Enemy Wave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private EnemySpawnSequence[] _spawnSequences;

        public State Begin(Level level) => new State(this, level);

        [Serializable]
        public struct State
        {
            private Level _level;
            private EnemyWave _wave;
            private int _index;
            private EnemySpawnSequence.State _sequence;

            public State(EnemyWave wave, Level level)
            {
                _wave = wave;
                _index = 0;
                _level = level;
                _sequence = _wave._spawnSequences[0].Begin(_level);
            }

            public float Progress(float deltaTime)
            {
                deltaTime = _sequence.Progress(deltaTime);
                while (deltaTime >= 0f)
                {
                    if (++_index >= _wave._spawnSequences.Length)
                    {
                        return deltaTime;
                    }

                    _sequence = _wave._spawnSequences[_index].Begin(_level);
                    deltaTime = _sequence.Progress(deltaTime);
                }

                return -1;
            }
        }
    }
}