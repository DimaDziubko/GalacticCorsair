using System;
using _Game.Core.Services.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.GameResult.Scripts
{
    [RequireComponent(typeof(Canvas))]
    public class LevelCompleteWindow : MonoBehaviour
    {
        [SerializeField] private LevelCompleteIntroAnimation _introAnimation;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _quitButton;

        private Canvas _canvas;
        
        private Action _onNext;
        private Action _onQuit;

        public async void Show(Action onNext, Action onQuit)
        {
            _onNext = onNext;
            _onQuit = onQuit;

            _nextButton.interactable = false;
            _quitButton.interactable = false;

            _canvas.enabled = true;
            await _introAnimation.Play();
            
            _nextButton.interactable = true;
            _quitButton.interactable = true;
        }
        
        public void Initialize(IWorldCameraService cameraService)
        {
            _canvas = GetComponent<Canvas>();
            _canvas.worldCamera = cameraService.UICamera;
            _canvas.enabled = false;
            _nextButton.onClick.AddListener(OnNextClicked);
            _quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnNextClicked()
        {
            _onNext?.Invoke();
            _canvas.enabled = false;
        }

        private void OnQuitClicked()
        {
            _onQuit?.Invoke();
            _canvas.enabled = false;
        }
    }
}
