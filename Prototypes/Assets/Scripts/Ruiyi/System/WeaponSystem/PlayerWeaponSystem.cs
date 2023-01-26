using System;
using System.Collections.Generic;
using Framework;
using UnityEngine.Assertions;

namespace Ruiyi.System.WeaponSystem
{
    public class PlayerWeaponSystem : AbstractSystem, IPlayerWeaponSystem
    {
        private BindableProperty<IWeapon> _curWeapon;
        private readonly LinkedList<IWeapon> _weapons = new();
        protected override void OnInit()
        {
            PickNewWeapon(new Pistol());
        }

        public void SwitchNextWeapon()
        {
            throw new NotImplementedException();
        }

        public void SwitchPreviousWeapon()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            if (_curWeapon == null)
            {
                return;
            }
            // only reload when current weapon is not full
            // if (_curWeapon.Value.CanReload())
            // {
            //     _curWeapon.Value.Reload();
            // }
            //
            // _curWeapon.Value.Reload();
        }

        public void PickNewWeapon(IWeapon weapon)
        {
            _weapons.AddFirst(weapon);
            _curWeapon.Value = weapon;
        }

        public IWeapon DropCurrentWeapon()
        {
            if (_curWeapon == null)
            {
                return null;
            }
            Assert.IsTrue(_weapons.Count > 0);
            var weapon = _curWeapon.Value;
            _weapons.RemoveFirst();
            _curWeapon.Value = _weapons.First?.Value;
            return weapon;
        }

        public BindableProperty<IWeapon> GetCurWeapon()
        {
            return _curWeapon;
        }
    }
}