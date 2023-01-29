using System;
using Framework;
using Ruiyi.Controller.GameController;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class MotionPlayerController : MonoBehaviour, IController
    {
        public int mCurrentWeaponIndex;
        public GameObject[] mWeapons;
        public GameObject mGroundCheck;
        public GameObject mCeilingCheck;
    
        public float operationTimeout = 0.2f;
        public float crouchDuration = 0.5f;
        
        private Rigidbody2D _mRigidbody2D;
        private Animator _mAnimator;
        private TriggerCheck2D _mGroundTriggerCheck2D;
        private TriggerCheck2D _mCeilingTriggerCheck2D;
        private IWeaponController _mGunController;

        private ExpiableProperty<bool> _mJumpDown;
        private ExpiableProperty<bool> _mReloadDown;
        private bool _mCrouchPressed;
        private bool _mFirePressed;
        private Vector2 _mMousePosition;
        
        private float _mCrouchTimer;
        private static readonly int Crouching = Animator.StringToHash("Crouching");
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _mRigidbody2D = GetComponent<Rigidbody2D>();
            _mAnimator = GetComponentInChildren<Animator>();
            
            _mGroundTriggerCheck2D = mGroundCheck.GetComponent<TriggerCheck2D>();
            _mCeilingTriggerCheck2D = mCeilingCheck.GetComponent<TriggerCheck2D>();

            _mJumpDown = new(operationTimeout);
            _mReloadDown = new(operationTimeout);
            _mCrouchPressed = false;
            _mFirePressed = false;
            _mCrouchTimer = 0f;
            
            for (int i = 0; i < mWeapons.Length; i++)
            {
                mWeapons[i].SetActive(false);
            }
            mWeapons[mCurrentWeaponIndex].SetActive(true);
            _mGunController = mWeapons[mCurrentWeaponIndex].GetComponent<GunController>();
        }
        
        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _mJumpDown.Value = true;
            }
            if (Input.GetButtonDown("Reload"))
            {
                _mReloadDown.Value = true;
            }
            
            // if (Input.GetButtonDown("Fire1"))
            // {
            //     _mFirePressed = true;
            // }
            // else if (Input.GetButtonUp("Fire1"))
            // {
            //     _mFirePressed = false;
            // }
            
            
            if (Input.GetButtonDown("Crouch"))
            {
                _mCrouchPressed = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                _mCrouchPressed = false;
            }

            // if (Input.GetKeyDown(KeyCode.E))
            // {
            //     // TODO: switch next weapon
            //     mWeapons[mCurrentWeaponIndex].SetActive(false);
            //     mCurrentWeaponIndex = (mCurrentWeaponIndex + 1) % mWeapons.Length;
            //     mWeapons[mCurrentWeaponIndex].SetActive(true);
            //     _mGunController = mWeapons[mCurrentWeaponIndex].GetComponent<GunController>();
            // }
            //
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     // TODO: switch previous weapon
            //     mWeapons[mCurrentWeaponIndex].SetActive(false);
            //     mCurrentWeaponIndex = (mCurrentWeaponIndex - 1 + mWeapons.Length) % mWeapons.Length;
            //     mWeapons[mCurrentWeaponIndex].SetActive(true);
            //     _mGunController = mWeapons[mCurrentWeaponIndex].GetComponent<GunController>();
            // }

            _mMousePosition = _camera!.ScreenToWorldPoint(Input.mousePosition);
        }

        private void OnGUI()
        {
            // set font size
            GUI.skin.label.fontSize = 36;
            // add a label to display the ammo
            GUI.Label(new Rect(40, 35, 400, 50), _mGunController.StateString());
        }

        private void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
        
            _mRigidbody2D.velocity = new Vector2(horizontal * 5, _mRigidbody2D.velocity.y);
            // to check if the player is on the ground by layer mask
            
            if (horizontal * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
            }
            
            // Jump
            if (_mJumpDown.Value && _mGroundTriggerCheck2D.Triggered())
            {
                _mJumpDown.Value = false;
                // mRigidbody2D.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                _mRigidbody2D.velocity = new Vector2(_mRigidbody2D.velocity.x, 5);
            }
            
            // Crouch
            if (!_mCrouchPressed && !_mCeilingTriggerCheck2D.Triggered())
            {
                // stand up
                _mCrouchTimer -= Time.fixedDeltaTime;
                if (_mCrouchTimer <= 0)
                {
                    _mCrouchTimer = 0;
                }
            } 
            else if (_mCrouchPressed)
            {
                // crouch
                _mCrouchTimer += Time.fixedDeltaTime;
                if (_mCrouchTimer >= crouchDuration)
                {
                    _mCrouchTimer = crouchDuration;
                }
            }
            
            _mAnimator.SetFloat(Crouching, _mCrouchTimer / crouchDuration);
            
            if (_mFirePressed && _mGunController.State == WeaponState.Idle)
            {
                _mGunController.Attack();
            }
            
            if (_mReloadDown.Value && _mGunController.State == WeaponState.Idle)
            {
                _mGunController.Reload();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return ProgenyPlatform.Interface;
        }
    }
}
