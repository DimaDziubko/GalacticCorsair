using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Core.Loading.Scripts
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _progressFill;
        [SerializeField] private TextMeshProUGUI _loadingInfo;
        [SerializeField] private TextMeshProUGUI _loadingProgress;
        [SerializeField] private float _barSpeed;

        private float _targetProgress;

        public async UniTask Load(
            Queue<ILoadingOperation> loadingOperations, 
            Camera uICamera)
        {
            _canvas.worldCamera = uICamera;
            _canvas.enabled = true;
            StartCoroutine(UpdateProgressBar());

            foreach (var operation in loadingOperations)
            {
                ResetFill();
                _loadingInfo.text = operation.Description;
                await operation.Load(OnProgress);
                await WaitForBarFill();
            }
        }

        private void ResetFill()
        {
            _progressFill.value = 0;
            _targetProgress = 0;
        }

        private void OnProgress(float progress)
        {
            _targetProgress = progress;
            _loadingProgress.text = $"{_targetProgress * 100}%";
        }

        private async UniTask WaitForBarFill()
        {
            while (_progressFill.value < _targetProgress)
            {
                await UniTask.Yield();
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        }
        
        private IEnumerator UpdateProgressBar()
        {
            while (_canvas.enabled)
            {
                if(_progressFill.value < _targetProgress)
                    _progressFill.value += Time.deltaTime * _barSpeed;
                yield return null;
            }
        }
    }
}
