namespace _Game.Core.Services.Camera
{
    public class WorldCameraService : IWorldCameraService
    {
        public UnityEngine.Camera UICamera => _uICamera;
        public float CameraHeight => Camera.orthographicSize;
        public float CameraWidth =>  CameraHeight * Camera.aspect;
        private UnityEngine.Camera Camera => UnityEngine.Camera.main;
        private UnityEngine.Camera _uICamera;

        public WorldCameraService(UnityEngine.Camera uICamera)
        {
            _uICamera = uICamera;
        }
        
    }
}