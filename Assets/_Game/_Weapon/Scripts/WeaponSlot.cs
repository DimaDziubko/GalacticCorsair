using _Game._Weapon.Scripts.Factory;
using UnityEngine;

namespace _Game._Weapon.Scripts
{
    public class WeaponSlot : MonoBehaviour
    {
        public WeaponType WeaponType
        {
            get => _weaponType;
        }
        public Weapon Weapon
        {
            get => _weapon;
            private set => _weapon = value;
        }

        private IWeaponFactory _weaponFactory;
        private Weapon _weapon;
        private AudioSource _audioSource;

        private bool _needWeaponModel;
        private WeaponType _weaponType;

        public void Construct(IWeaponFactory weaponFactory, bool needWeaponModel)
        {
            _weaponFactory = weaponFactory;
            _needWeaponModel = needWeaponModel;
            _weaponType = WeaponType.None;
        }
        
        public void SetWeapon(WeaponType type)
        {
            Weapon = _weaponFactory.Get(type, transform);
            _weaponType = type;
            Weapon.ModelVisibility = _needWeaponModel;
        }

        public void RemoveWeapon()
        {
            if (Weapon != null)
            {
                _weaponFactory.Reclaim(_weapon);
            }
        }
        
    }
}