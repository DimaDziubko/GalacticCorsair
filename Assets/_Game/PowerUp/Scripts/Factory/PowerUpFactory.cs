using System.Collections.Generic;
using _Game.Common;
using _Game.Core.Factory;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Random;
using UnityEngine;

namespace _Game.PowerUp.Scripts.Factory
{
    [CreateAssetMenu(fileName = "PowerUpFactory", menuName = "Factories/PowerUp Factory")]
    public class PowerUpFactory : GameObjectFactory, IPowerUpFactory
    {
        private IStaticDataService _staticData;
        private IWorldCameraService _cameraService;
        private IRandomService _randomService;
        
        private readonly List<PowerUpConfig> _powerUpFrequency = new List<PowerUpConfig>();
        
        
        private readonly GameBehaviourCollection _powerUpContainer = new GameBehaviourCollection();

        public GameBehaviourCollection PowerUpContainer => _powerUpContainer;
        
        public void Construct(
            IStaticDataService staticData,
            IWorldCameraService cameraService,
            IRandomService randomService)
        {
            _staticData = staticData;
            _cameraService = cameraService;
            _randomService = randomService;

            PowerUpConfig[] configs = _staticData.ForPowerUps;
            foreach (var config in configs)
            {
                for (int i = 0; i < config.PowerUpFrequency; i++)
                {
                    _powerUpFrequency.Add(config);
                }
            }
        }
        
        public Powerup Get()
        {
            int powerUpIndex = _randomService.Next(0, _powerUpFrequency.Count);
            PowerUpConfig config = _powerUpFrequency[powerUpIndex];
            Powerup instance = CreateGameObjectInstance(config.PowerupPrefab);
            instance.Construct(
                config.Type,
                config.WeaponType,
                _cameraService,
                _randomService,
                config.LifeTime,
                config.DriftMin,
                config.DriftMax);
            instance.OriginFactory = this;
            _powerUpContainer.Add(instance);
            return instance;
        }

        public void Reclaim (Powerup powerUp) => 
            Destroy(powerUp.gameObject);
    }
}