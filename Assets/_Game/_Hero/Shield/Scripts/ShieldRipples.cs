using UnityEngine;

namespace _Game._Hero.Shield.Scripts
{
    public class ShieldRipples : MonoBehaviour
    {
        private const string SPHERE_CENTER = "_SphereCenter";

        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private ParticleSystemRenderer _renderer;
        private Material mat => _renderer.material;
        
        public void ShowRipple()
        {
            _particle.Play();
        }

        public void HideRipple()
        {
            _particle.Stop();
        }

        public void Adjust(Vector3 at)
        {
            mat.SetVector(SPHERE_CENTER, at);
        }
    }
}
