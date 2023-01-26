using Framework;
using UnityEngine;

namespace Controller
{
    
    public class BulletGunController : MonoBehaviour, IGunController
    {
        public GameObject mBulletPrefab;
        
        public int mBulletCount = 10;
        public int mBulletMaxCount = 10;
        public float mReloadTime = 1f;
        public float mReloadTimer = 0f;
        
        // public float mFireRate = 0.2f;
        
        public void Fire()
        {
            if (!CanFire())
            {
                return;
            }
            
            // TODO : mBulletPrefab = Resources.Load<>()
            
            var bullet = Instantiate(mBulletPrefab, transform.position, transform.rotation);
            bullet.transform.localScale = mBulletPrefab.transform.lossyScale;

            bullet.SetActive(true);

            mBulletCount--;
            if (mBulletCount <= 0)
            {
                mReloadTimer = mReloadTime;
            }
        }
        
        
        private void Update()
        {
            mReloadTimer -= Time.deltaTime;
        }

        public bool CanFire()
        {
            if (mBulletCount <= 0)
            {
                return false;
            }
            if (mReloadTimer > 0)
            {
                return false;
            }
            return true;
        }

        public IArchitecture GetArchitecture()
        {
            return ProgenyPlatform.Interface;
        }
    }
}