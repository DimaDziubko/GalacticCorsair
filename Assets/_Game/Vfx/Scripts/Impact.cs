using UnityEngine;

namespace _Game.Vfx.Scripts
{
    public class Impact : VfxEntity
    {
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
