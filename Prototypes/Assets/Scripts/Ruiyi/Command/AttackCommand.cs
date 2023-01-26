using Framework;
using Ruiyi.System.WeaponSystem;

namespace Ruiyi.Command
{
    public class AttackCommand : AbstractCommand
    {
        public static readonly AttackCommand Single = new();
        private readonly IPlayerWeaponSystem _gunSystem;

        private AttackCommand()
        {
            _gunSystem = this.GetSystem<IPlayerWeaponSystem>();
        }
        protected override void OnExecute()
        {
            _gunSystem.Attack();
        }
    }
}