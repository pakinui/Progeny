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
            int bulletMaxCount, 
            float bulletSpeed, 
            float damage, 
            float shootCoolDown, 
            float reloadCoolDown, 
            float shootDistance) 
        {
            Name = name;
            
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
            { "Pistol", new GunConfigItem("Pistol", 6, 8, 1, 0.5f, 4, 5) },
            { "Pistol", new GunConfigItem("Pistol", 6, 8, 1, 0.5f, 4, 5) },
            
        };

        public GunConfigItem GetItemByName(string gunName)
        {
            return mItems[gunName];
        }
    }
}