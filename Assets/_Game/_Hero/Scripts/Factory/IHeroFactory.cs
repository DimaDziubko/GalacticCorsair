using _Game.Core.Services;

namespace _Game._Hero.Scripts.Factory
{
    public interface IHeroFactory : IService
    {
        Hero Get(HeroType type);
        void Reclaim(Hero hero);
    }
}