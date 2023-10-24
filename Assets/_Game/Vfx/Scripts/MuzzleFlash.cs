using UnityEngine;

namespace _Game.Vfx.Scripts
{
    public class MuzzleFlash : VfxEntity
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        
        private float _age;

        public override bool GameUpdate()
        {
            _age += Time.deltaTime;
            if (_age >= Duration)
            {
                OriginFactory.Reclaim(this);
                return false;
            }
            return true;
        }
        
    }
}