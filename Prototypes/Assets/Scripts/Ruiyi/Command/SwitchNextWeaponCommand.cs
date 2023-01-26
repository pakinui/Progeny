using Framework;
using Ruiyi.System;
using Ruiyi.System.WeaponSystem;
using NotImplementedException = System.NotImplementedException;

namespace Ruiyi.Command
{
    public class SwitchNextWeaponCommand : AbstractCommand
    {
        public static readonly SwitchNextWeaponCommand Single = new();
        private readonly IPlayerWeaponSystem _gunSystem;

        private SwitchNextWeaponCommand()
        {
            _gunSystem = this.GetSystem<IPlayerWeaponSystem>();
        }
        protected override void OnExecute()
        {
            _gunSystem.SwitchNextWeapon();
        }
    }
}