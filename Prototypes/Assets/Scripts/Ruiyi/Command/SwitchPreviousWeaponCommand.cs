using Framework;
using Ruiyi.System;
using Ruiyi.System.WeaponSystem;
using NotImplementedException = System.NotImplementedException;

namespace Ruiyi.Command
{
    public class SwitchPreviousWeaponCommand : AbstractCommand
    {
        public static readonly SwitchPreviousWeaponCommand Single = new();
        private readonly IPlayerWeaponSystem _gunSystem;

        private SwitchPreviousWeaponCommand()
        {
            _gunSystem = this.GetSystem<IPlayerWeaponSystem>();
        }
        protected override void OnExecute()
        {
            _gunSystem.SwitchPreviousWeapon();
        }
    }
}