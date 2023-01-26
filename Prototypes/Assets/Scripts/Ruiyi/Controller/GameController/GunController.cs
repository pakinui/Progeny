using Framework;
using Ruiyi.Command;
using Ruiyi.System.WeaponSystem;
using UnityEngine;
using UnityEngine.Serialization;
using NotImplementedException = System.NotImplementedException;

namespace Ruiyi.Controller.GameController
{
    
    public class GunController : MonoBehaviour, IWeaponController
    {
        public GameObject mBullet;
        public GameObject mMuzzleFlash;
        
        public int mMagazineCapacity = 10;
        public float mReloadTime = 1f;
        public float mFireFrequency = 10;
        
        public int mBulletCount = 10;
        
        private Transform _muzzleTrans;
        private Animator _mAnimator;
        // private Animator _mAnimator;
        private ExpiableProperty<WeaponState> _mGunState;
        private static readonly int Fire = Animator.StringToHash("Fire");

        private void Awake()
        {
            _muzzleTrans = transform.Find("Muzzle");
            // _mAnimator = GetComponent<Animator>();
            _mGunState = new(1f / mFireFrequency, WeaponState.Idle);
            _mAnimator = _muzzleTrans.GetComponentInChildren<Animator>();
        }

        public IArchitecture GetArchitecture()
        {
            return ProgenyPlatform.Interface;
        }
        
        public WeaponState State
        {
            get => _mGunState.Value;
            set => _mGunState.Value = value;
        }

        public void Prepare()
        {
            // TODO
        }
        
        public void Attack()
        {
            if (mBulletCount <= 0)
            {
                Reload();
            }
            
            if (State != WeaponState.Idle)
            {
                return;
            }

            // TODO : mBullet = Resources.Load<>()
            
            var bullet = Instantiate(mBullet, _muzzleTrans.position, mBullet.transform.rotation);
            bullet.transform.localScale = mBullet.transform.lossyScale;

            bullet.SetActive(true);

            mBulletCount--;
            _mAnimator.SetTrigger(Fire);
            _mGunState.SetWithTimeout(WeaponState.Attack, 1f / mFireFrequency);
        }

        public void Reload()
        {
            // TODO : add out gun ammo capacity
            if (State != WeaponState.Idle)
            {
                return;
            }
            
            mBulletCount = mMagazineCapacity;
            
            // TODO : add reload animation
            
            // _mAnimator.SetTrigger("Reload");
            
            _mGunState.SetWithTimeout(WeaponState.Reload, mReloadTime);
        }

        private void Update()
        {
            // Debug.Log(nameof(_mGunState.Value) + _mGunState.Value);
        }

        public void SwitchNextWeapon()
        {
            this.SendCommand(SwitchNextWeaponCommand.Single);
        }

        public void SwitchPreviousWeapon()
        {
            this.SendCommand(SwitchPreviousWeaponCommand.Single);
        }

        public string StateString()
        {
            // ammo, state
            return $"{mBulletCount}/{mMagazineCapacity} {_mGunState.Value}";
        }
    }
}