using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using UnityEngine;

namespace _Game.Enemies.Scripts
{
    public class EnemyLinearMove : MonoBehaviour
    {
        private float _speed;
        private IWorldCameraService _cameraService;
        private IRandomService _randomService;

        private Vector3 _direction;
        
        public void Initialize(
            float speed, 
            IWorldCameraService cameraService, 
            IRandomService randomService)
        {
            _speed = speed;
            _cameraService = cameraService;
            _randomService = randomService;
        }
         public Vector3 Position
         {
             get => transform.position;
             set => transform.position = value;
         }

         public void GameUpdate()
         {
             Move();
         }

         private void Move()
         {
             if (_direction == Vector3.zero)
             {
                 Vector3 guidePoint = SelectGuidePoint();
                 _direction = guidePoint - Position;
                 _direction.Normalize();
             }
             
             Vector3 tempPos = Position;
             tempPos += _direction * (_speed * Time.deltaTime);
             Position = tempPos;
             
             Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.back);
             transform.rotation = targetRotation;
         }

         private Vector3 SelectGuidePoint()
         {
             return new Vector3 (_randomService.Next(-_cameraService.CameraWidth / 2, _cameraService.CameraWidth / 2),
                 _randomService.Next(-_cameraService.CameraHeight / 2, _cameraService.CameraHeight / 2), 0);
         }
    }
}