namespace _Game.Core.Services.Camera
{
    public class WorldCameraService : IWorldCameraService
    {
        private UnityEngine.Camera Camera => UnityEngine.Camera.main;
        public float CameraHeight => Camera.orthographicSize;
        public float CameraWidth =>  CameraHeight * Camera.aspect;
        
    }
}