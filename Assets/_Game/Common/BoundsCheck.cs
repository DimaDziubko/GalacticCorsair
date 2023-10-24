using System;
using _Game.Core.Services.Camera;
using UnityEngine;

namespace _Game.Common
{
    public class BoundsCheck : MonoBehaviour
    {
        public event Action OffScreen;
        
        [Header("Set in Inspector")]
        public float Radius = 1f;
        public bool KeepOnScreen = true;

        [Header("Set Dynamically")]
        public bool IsOnScreen = true;

        private IWorldCameraService _cameraService;
        
        private bool _offRight, _offLeft, _offUp, _offDown;

        public void Construct(IWorldCameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void GameLateUpdate()
        {
            Vector3 pos = transform.position;
            IsOnScreen = true;
            _offRight = _offLeft = _offDown = _offUp = false;

            if(pos.x > _cameraService.CameraWidth - Radius)
            {
                pos.x = _cameraService.CameraWidth  - Radius;
                IsOnScreen = false;
                _offRight = true;
                OffScreen?.Invoke();
            }
            if (pos.x < -_cameraService.CameraWidth  + Radius)
            {
                pos.x = -_cameraService.CameraWidth  + Radius;
                IsOnScreen = false;
                _offLeft = true;
                OffScreen?.Invoke();
            }
            if (pos.y > _cameraService.CameraHeight  - Radius)
            {
                pos.y = _cameraService.CameraHeight - Radius;
                IsOnScreen = false;
                _offUp = true;
                OffScreen?.Invoke();
            }
            if (pos.y < -_cameraService.CameraHeight + Radius)
            {
                pos.y = -_cameraService.CameraHeight + Radius;
                IsOnScreen = false;
                _offDown = true;
                OffScreen?.Invoke();
            }

            IsOnScreen = !(_offRight || _offLeft || _offUp || _offDown);

            if(KeepOnScreen && !IsOnScreen)
            {
                transform.position = pos;
                IsOnScreen = true;
                _offRight = _offLeft = _offDown = _offUp = false;
            }       
        }

        // void OnDrawGizmos()
        // {
        //     if (!Application.isPlaying) return;
        //     Vector3 boundSize = new Vector3(_cameraService.CameraWidth * 2, _cameraService.CameraHeight * 2, 0.1f);
        //
        //     Gizmos.DrawWireCube(Vector3.zero, boundSize);
        // }
    }
}
