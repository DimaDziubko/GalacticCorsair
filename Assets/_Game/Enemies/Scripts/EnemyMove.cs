using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    public abstract class EnemyMove : MonoBehaviour
    {
        private IWorldCameraService _cameraService;
        private IRandomService _randomService;
        protected float _speed;
        
        protected Vector3 _direction;
        protected Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public virtual void Initialize(
            EnemyMovementConfig movementConfig, 
            IWorldCameraService cameraService, 
            IRandomService randomService)
        {
            _cameraService = cameraService;
            _randomService = randomService;
            _speed = movementConfig.Speed.RandomValueInRange;
        }
        
        public void GameUpdate()
        {
            Move();
        }

        protected abstract void Move();
        
        protected Vector3 SelectGuidePoint()
        {
            return new Vector3 (_randomService.Next(-_cameraService.CameraWidth / 2, _cameraService.CameraWidth / 2),
                _randomService.Next(-_cameraService.CameraHeight / 2, _cameraService.CameraHeight / 2), 0);
        }
    }
}