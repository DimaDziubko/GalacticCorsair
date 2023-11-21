using _Game.Core.Services.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI.Hud.Scripts
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _playerHealth;
        [SerializeField] private Slider _shielCapacity;

        public void Initialize(IWorldCameraService cameraService)
        {
            _canvas.worldCamera = cameraService.UICamera;
        }

        public void UpdatePlayerHealth(float currentHp, float maxHp)
        {
            Debug.Log($"Current player hp = {currentHp}, max player hp = {maxHp}");
            _playerHealth.value = currentHp / maxHp; 
        }

        public void UpdateShieldCapacity(float currentCapacity, float maxCapacity)
        {
            Debug.Log($"Current shield capacity = {currentCapacity}, max shield capacity = {maxCapacity}");
            _shielCapacity.value = currentCapacity / maxCapacity; 
        }

        public void Reset()
        {
            _playerHealth.value = 1;
            _shielCapacity.value = 1;
        }
    }
}