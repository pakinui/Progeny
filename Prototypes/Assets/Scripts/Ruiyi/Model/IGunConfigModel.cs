using System.Collections.Generic;
using Framework;


namespace Ruiyi.Model
{
    public interface IGunConfigModel : IModel
    {
        GunConfigItem GetItemByName(string gunName);
    }

    public class GunConfigItem
    {
        public GunConfigItem(
            string name, 
            int magazineCapacity, 
            float bulletSpeed, 
            float damage, 
            float shootCoolDown, 
            float reloadCoolDown, 
            float shootDistance) 
        {
            Name = name;
            MagazineCapacity = magazineCapacity;
            BulletSpeed = bulletSpeed;
            Damage = damage;
            ShootCoolDown = shootCoolDown;
            ReloadCoolDown = reloadCoolDown;
            ShootDistance = shootDistance;
        }
        
        public string Name { get; set; }
        public int MagazineCapacity { get; set; }
        public float BulletSpeed { get; set; }
        public float Damage { get; set; }
        public float ShootCoolDown { get; set; }
        public float ReloadCoolDown { get; set; }
        public float ShootDistance { get; set; }
    }

    public class GunConfigModel : AbstractModel, IGunConfigModel
    {
        protected override void OnInit()
        {
            
        }

        private Dictionary<string, GunConfigItem> mItems = new Dictionary<string, GunConfigItem>()
        {
            { "Pistol", new GunConfigItem("Pistol", 6, 8, 1, 0.5f, 4, 5) },
            { "Sniper", new GunConfigItem("Sniper", 1, 20, 10, 1, 4, 10) },
            // { "Shotgun", new GunConfigItem("Shotgun", 2, 5, 2, 1, 3, 5) },
            // { "MachineGun", new GunConfigItem("MachineGun", 30, 10, 1, 0.1f, 3, 5) },
        };

        public GunConfigItem GetItemByName(string gunName)
        {
            return mItems[gunName];
        }
    }
}