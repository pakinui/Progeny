using Framework;

namespace Ruiyi.Controller.GameController
{
    public enum WeaponState
    {
        Idle,
        Attack,
        Reload,
    }
    public interface IWeaponController : IController
    {
        WeaponState State { get; set; }
        void Prepare();
        void Attack();
        void Reload();
        
        void SwitchNextWeapon();
        void SwitchPreviousWeapon();
        
        string StateString();
    }
}