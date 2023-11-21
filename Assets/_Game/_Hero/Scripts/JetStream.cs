using UnityEngine;

namespace _Game._Hero.Scripts
{
    public class JetStream : MonoBehaviour
    {
        [SerializeField] private Transform[] jetStreams;

        public void GameUpdate(float currentSpeed, float maxSpeed)
        {
            AdjustSizeToSpeed(currentSpeed, maxSpeed);
        }

        private void AdjustSizeToSpeed(float currentSpeed, float maxSpeed)
        {
            foreach (var jetStream in jetStreams)
            {
                var factor = currentSpeed / maxSpeed;
                var localScale = jetStream.localScale;
                localScale = new Vector3(localScale.x, localScale.y, factor);
                jetStream.localScale = localScale;
            }
        }
    }
}