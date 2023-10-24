using _Game.Core.Services;
using UnityEngine;

namespace _Game._Weapon.Scripts.Factory
{
    public interface IWeaponFactory : IService
    {
        Weapon Get(WeaponType type, Transform parent);
        void Reclaim (Weapon weapon);
    }
}