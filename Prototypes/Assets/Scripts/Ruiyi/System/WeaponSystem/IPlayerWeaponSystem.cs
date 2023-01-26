using System.Collections.Generic;
using Framework;
using Unity.VisualScripting;
using UnityEngine.Assertions;
using NotImplementedException = System.NotImplementedException;

namespace Ruiyi.System.WeaponSystem
{
    public interface IPlayerWeaponSystem : ISystem
    {
        void SwitchNextWeapon();
        void SwitchPreviousWeapon();
        
        void Prepare(); // prepare to attack for higher accuracy and damage
        void Attack(); // attack with current weapon
        void Reload(); // reload current weapon
        
        void PickNewWeapon(IWeapon weapon);
        IWeapon DropCurrentWeapon();
        BindableProperty<IWeapon> GetCurWeapon();
    }

    
}