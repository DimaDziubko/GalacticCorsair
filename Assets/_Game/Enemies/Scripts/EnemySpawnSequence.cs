using System;
using _Game.Levels.Level_1.Scripts;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    [Serializable]
    public class EnemySpawnSequence
    {
        [SerializeField] private EnemyType _type;
        [SerializeField, Range(1, 100)] private int _amount = 1;
        [SerializeField, Range(0.1f, 10f)] private float _cooldown = 1f;

        public State Begin(Level level) => new State(this, level);

        [Serializable]
        public struct State
        {
            private Level _level;
            private EnemySpawnSequence _sequence;
            private int _count;
            private float _cooldown;
            public State(EnemySpawnSequence sequence, Level level)
            {
                _sequence = sequence;
                _count = 0;
                _cooldown = sequence._cooldown;
                _level = level;
            }

            public float Progress(float deltaTime)
            {
                _cooldown += deltaTime;
                while (_cooldown >= _sequence._cooldown)
                {
                    _cooldown -= _sequence._cooldown;
                    if (_count >= _sequence._amount)
                    {
                        return _cooldown;
                    }

                    _count++;
                    
                    _level.SpawnEnemy(_sequence._type);
                }

                return -1f;
            }
        }
    }
}