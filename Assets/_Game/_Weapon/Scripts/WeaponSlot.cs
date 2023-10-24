using _Game._Weapon.Scripts.Factory;
using UnityEngine;

namespace _Game._Weapon.Scripts
{
    public class WeaponSlot : MonoBehaviour
    {
        public Weapon Weapon
        {
            get => _weapon;
            private set => _weapon = value;
        }

        private IWeaponFactory _weaponFactory;
        private Weapon _weapon;
        private AudioSource _audioSource;

        public void Construct(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
        }
        
        public void SetWeapon(WeaponType type)
        {
            Weapon = _weaponFactory.Get(type, transform);
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