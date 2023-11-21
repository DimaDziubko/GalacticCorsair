using System;
using _Game.Common;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    [Serializable]
    public class EnemyMovementConfig
    {
        [Header("For all types of movement")]
        [FloatRangeSlider(5f, 20f)]
        public FloatRange Speed = new FloatRange(10f);
        
        [Header("For sinusoidal movement")]
        
        public float WaveFrequency = 2;
        
        public float WaveWidth = 4f;
        
        public float Roll = 45;
    }
}