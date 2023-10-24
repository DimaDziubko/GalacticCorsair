using _Game.Common;
using _Game.Vfx.Scripts.Factory;

namespace _Game.Vfx.Scripts
{
    public abstract class VfxEntity : GameBehaviour
    {
        public VfxFactory OriginFactory { get; set; }
        
        public float Duration { get; set; }

        public override void GameLateUpdate() { }

        public override void Recycle()
        {
            OriginFactory.Reclaim(this);
        }
    }
}