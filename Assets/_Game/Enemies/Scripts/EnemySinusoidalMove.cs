using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    public class EnemySinusoidalMove : EnemyMove
    {
        private float _waveFrequency;
        private float _waveWidth;
        private float _roll;

        private float _birthTime;
        private Vector3 _referencePosition;

        public override void Initialize(
            EnemyMovementConfig movementConfig, 
            IWorldCameraService cameraService, 
            IRandomService randomService)
        {
            base.Initialize(movementConfig, cameraService, randomService);
            
            _birthTime = Time.time;
            _waveFrequency = movementConfig.WaveFrequency;
            _waveWidth = movementConfig.WaveWidth;
            _roll = movementConfig.Roll;
        }

        protected override void Move()
        {
            if (_direction == Vector3.zero)
            {
                Vector3 guidePoint = SelectGuidePoint();
                _direction = guidePoint - Position;
                _direction.Normalize();
                _referencePosition = Position;
                
                Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.back);
                transform.rotation = targetRotation;
            }

            Vector3 perpendicular = Quaternion.Euler(0f, 0f, 90f) * _direction;
            float age = Time.time - _birthTime;
            float theta = Mathf.PI * 2 * age / _waveFrequency;
            float sin = Mathf.Sin(theta);
            _referencePosition += _direction * _speed * Time.deltaTime;
            Vector3 offset = perpendicular * (_waveWidth * sin);
            Vector3 tempPos = _referencePosition + offset;
            Position = tempPos;
            
            Vector3 euler = transform.rotation.eulerAngles;
            float xRotation = euler.x;
            float yRotation = euler.y;
            float zRotation = sin * _roll + 90f;

            Quaternion newRotation = Quaternion.Euler(xRotation, yRotation, zRotation);

            transform.rotation = newRotation;

        }
        
    }
}