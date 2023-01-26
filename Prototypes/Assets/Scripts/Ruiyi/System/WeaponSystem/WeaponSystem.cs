using Framework;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Ruiyi.System.WeaponSystem
{
    public class Pistol : IGunWeapon
    {
        
        public void Prepare()
        {
            
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public bool CanAttack()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }

        public bool CanReload()
        {
            throw new NotImplementedException();
        }

        public void SupplyAmmo()
        {
            throw new NotImplementedException();
        }

        public void Prepare(Vector2 aimPos)
        {
            throw new NotImplementedException();
        }

        public bool CanPrepare()
        {
            throw new NotImplementedException();
        }
    }
    public class WeaponSystem : AbstractSystem, IWeaponSystem
    {
        protected override void OnInit()
        {
            throw new NotImplementedException();
        }
    }
}