using UnityEngine;

namespace _Game.Core.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsFireButtonUp();
    }
}