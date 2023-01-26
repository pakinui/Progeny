using System;
using Framework;
using UnityEngine;

namespace Ruiyi.System.WeaponSystem
{
    public enum WeaponType
    {
        Gun,
        Melee,
        Grenade
    }
    
    
    public interface IWeapon
    {
        void Attack();
        bool CanAttack();
    }
    
    public interface INeedReload
    {
        void Reload();
        bool CanReload();
    }
    
    public interface INeedAim
    {
        void Prepare(Vector2 aimPos);
        bool CanPrepare();
    }

    public interface INeedCharge
    {
        void Charge();
        bool CanCharge();
    }
    
    public interface IGunWeapon : IWeapon, INeedAim, INeedReload
    {
        
    }
     
    
    public interface IWeaponSystem : ISystem
    {
        
    }
    
    
}