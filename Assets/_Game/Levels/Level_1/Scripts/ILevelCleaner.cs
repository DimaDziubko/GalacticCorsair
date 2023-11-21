using System.Collections.Generic;
using _Game.Core.Factory;

namespace _Game.Levels.Level_1.Scripts
{
    public interface ILevelCleaner
    {
        IEnumerable<GameObjectFactory> Factories { get; }
        string SceneName { get; }
        void Cleanup();
    }
}