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
                weapon.Construct(weaponFactory);
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
            foreach (var slot in _weapons)
            {
                slot.SetWeapon(WeaponType.Blaster);
            }
            
            foreach (var slot in _weapons)
            {
                if (slot.Weapon != null)
                {
                    NeedFire += slot.Weapon.Fire;
                }
            }
            
            // var slot = _weapons[0];
            // slot.SetWeapon(WeaponType.Blaster);
            // NeedFire += slot.Weapon.Fire;
            //
        }

        [Button]
        private void ClearWeapon()
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

    }
}