using System;
using UnityEngine;

namespace _Game._Hero.Scripts
{
    [Serializable]
    public class HeroMovementConfig
    {
        [Range(15f, 50f)]
        public float Speed = 30f;
            
        [Range(-90f, 90f)]
        public float RollMax = -45f;
            
        [Range(0f, 60f)]
        public float PitchMax = 30f;
        
        public float YawSpeed = 180f;
        public float PitchSmoothFactor = 1f;
        public float RollSmoothFactor = 1f;
        
        public float Acceleration = 2f;
        public float Deceleration = 3f;
        public float PitchMaxDeviationAngle = 20f;
        public float RollDeviation = 0.1f;
        public float SpeedInterpolationFactor = 0.05f;
    }
}