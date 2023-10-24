using _Game.Core.Services;

namespace _Game.Enemies.Scripts.Factory
{
    public interface IEnemyFactory : IService
    {
        Enemy Get(EnemyType type);
        void Reclaim (Enemy enemy);
    }
}