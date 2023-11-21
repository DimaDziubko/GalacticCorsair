using _Game.Common;
using _Game.Core.Services;

namespace _Game.PowerUp.Scripts.Factory
{
    public interface IPowerUpFactory : IService
    {
        Powerup Get();
        void Reclaim(Powerup powerUp);
        GameBehaviourCollection PowerUpContainer { get;}
    }
}