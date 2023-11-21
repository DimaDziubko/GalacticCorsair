using _Game._Weapon.Scripts.Factory;
using _Game.Core.Factory;
using _Game.Core.Services._Game.StaticData;
using _Game.Core.Services.Audio;
using _Game.Core.Services.Camera;
using _Game.Core.Services.Input;
using UnityEngine;

namespace _Game._Hero.Scripts.Factory
{
    [CreateAssetMenu(fileName = "HeroFactory", menuName = "Factories/Hero Factory")]
    public class HeroFactory : GameObjectFactory, IHeroFactory
    {
        private IInputService _inputService;
        private IWeaponFactory _weaponFactory;
        private IStaticDataService _staticData;
        private IWorldCameraService _cameraService;
        private IVfxAudioSourceService _audioSourceService;

        public void Construct(
            IInputService inputService, 
            IWeaponFactory weaponFactory,
            IStaticDataService staticData,
            IWorldCameraService cameraService,
            IVfxAudioSourceService audioSourceService)
        {
            _inputService = inputService;
            _weaponFactory = weaponFactory;
            _staticData = staticData;
            _cameraService = cameraService;
            _audioSourceService = audioSourceService;

        }

        public Hero Get(HeroType type)
        {
            HeroConfig config = _staticData.ForHero(type);
            
            Hero instance = CreateGameObjectInstance(config.Prefab);
            instance.Construct(
                _inputService,
                config.Health,
                config.ShieldCapacity,
                config.RippleDuration,
                _weaponFactory,
                _cameraService,
                _audioSourceService,
                config.ShieldHitSound,
                config.MovementConfig);
            instance.OriginFactory = this;
            
            return instance;
        }

        public void Reclaim ( Hero hero ) => 
            Destroy(hero.gameObject);
    }
}