using System;
using _Game._Weapon.Scripts;
using _Game._Weapon.Scripts.Factory;
using _Game.Core.Services.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game._Hero.Scripts
{
    public class HeroWeapon : MonoBehaviour
    {
        public bool NeedWeaponModel;
        public event Action<Vector3> NeedFire;
        
        private IInputService _inputService;
        private HeroMove _heroMove;
        
        
        [SerializeField] private WeaponSlot[] _weapons;

        public void Construct(
            IInputService inputService,
            IWeaponFactory weaponFactory,
            HeroMove heroMove)
        {
            _inputService = inputService;
            _heroMove = heroMove;
            InitializeWeaponSlots(weaponFactory);
            ResetWeapon();
        }

        private void InitializeWeaponSlots(IWeaponFactory weaponFactory)
        {
            foreach (var weapon in _weapons)
            {
                weapon.Construct(weaponFactory, NeedWeaponModel);
            }
        }

        public void GameUpdate()
        {
            if (_inputService.IsFireButtonUp())
            {
                NeedFire?.Invoke(_heroMove.Direction);
            }
        }

        private void ResetWeapon()
        {
            ClearWeapon();
            var slot = _weapons[0];
            slot.SetWeapon(WeaponType.Blaster);
            NeedFire += slot.Weapon.Fire;
            
        }

        [Button]
        public void ClearWeapon()
        {
            foreach (WeaponSlot slot in _weapons)
            {
                slot.RemoveWeapon();
                if (slot.Weapon != null)
                {
                    NeedFire -= slot.Weapon.Fire;
                }
            } 
        }

        public void Upgrade(WeaponType powerUpWeaponType)
        {
            if (_weapons[0].WeaponType != powerUpWeaponType)
            {
                ClearWeapon();
                SetupWeapon(powerUpWeaponType, 0);
                return;
            }

            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i].Weapon == null)
                {
                    SetupWeapon(powerUpWeaponType, i);
                    break;
                }
            }
            
        }

        private void SetupWeapon(WeaponType weaponType, int slotIndex)
        {
            _weapons[slotIndex].SetWeapon(weaponType);
            NeedFire += _weapons[slotIndex].Weapon.Fire;
        }
    }
}