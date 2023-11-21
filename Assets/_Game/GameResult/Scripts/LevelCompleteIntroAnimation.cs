using System;
using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace _Game.GameResult.Scripts
{
    [RequireComponent(typeof(PlayableDirector))]
    public class LevelCompleteIntroAnimation : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _infoText;
    
        private const string INFO_TEXT = "Level \n completed!";
        [SerializeField] private float _infoTextDelay = 1f; 
        [SerializeField] private float _infoTextDuration = 4f; 
    
        [SerializeField] private PlayableDirector _director;
        private UniTaskCompletionSource<bool> _playAwaiter;

        public async UniTask Play()
        {
            _playAwaiter = new UniTaskCompletionSource<bool>();
            _director.stopped -= OnTimelineFinished;
            _director.stopped += OnTimelineFinished;
        
            _director.Play();
            
            await UniTask.Delay(TimeSpan.FromSeconds(_infoTextDelay));
            await AnimateInfoText(INFO_TEXT, _infoTextDuration);
        
            await _playAwaiter.Task;
        }

        private async UniTask AnimateInfoText(string infoText, float infoTextDuration)
        {
            float timer = 0f;
            int totalCharacters = infoText.Length;
            StringBuilder animatedText = new StringBuilder(infoText.Length);
    
            while (timer < infoTextDuration)
            {
                float percentageComplete = timer /infoTextDuration;
                int charactersShown = (int)(totalCharacters * percentageComplete);
    
                animatedText.Length = 0; 
                animatedText.Append(infoText, 0, charactersShown + 1);

                _infoText.text = animatedText.ToString();
                
                timer += Time.deltaTime;
                await UniTask.Yield();
            }
        
            _infoText.text = infoText;
        }
    
        private void OnTimelineFinished(PlayableDirector _)
        {
            _playAwaiter.TrySetResult(true);
        }
    }
}
