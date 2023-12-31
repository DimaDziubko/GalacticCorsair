﻿using _Game.Enemies.Scripts;
using _Game.Levels.Level_1.Scripts;
using UnityEngine;

namespace _Game.Core.Scenario
{
    [CreateAssetMenu(fileName = "GameScenario", menuName = "Core/Game Scenario")]
    public class GameScenario : ScriptableObject
    {
        [SerializeField] private EnemyWave[] _waves;

        public State Begin(Level level)
        {
            return new State(this, level);
        }

        public struct State
        {
            private readonly Level _level;
            private readonly GameScenario _scenario;
            private int _index;
            private EnemyWave.State _wave;

            public (int currentWave, int wavesCount) GetWaves()
            {
                return (_index + 1, _scenario._waves.Length + 1);
            }

            public State(GameScenario scenario, Level level)
            {
                _scenario = scenario;
                _index = 0;
                _level = level;
                _wave = scenario._waves[0].Begin(_level);
            }

            public bool Progress()
            {
                float deltaTime = _wave.Progress(Time.deltaTime);
                while (deltaTime >= 0)
                {
                    if (++_index >= _scenario._waves.Length)
                    {
                        return false;
                    }

                    _wave = _scenario._waves[_index].Begin(_level);
                    deltaTime = _wave.Progress(deltaTime);
                }

                return true;
            }
        }
    }
}