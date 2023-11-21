namespace _Game.Core.Services.Camera
{
    public interface IWorldCameraService : IService
    {
        float CameraHeight { get; }
        float CameraWidth { get; }
        
        UnityEngine.Camera UICamera { get; }
    }
}