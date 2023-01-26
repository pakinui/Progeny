using Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour, IController
    {
        public GameObject mGun;
        public GameObject mGroundCheck;
        public GameObject mCeilingCheck;
    
        public float operationTimeout = 0.2f;
        public float crouchDuration = 0.5f;
        
        private Rigidbody2D _mRigidbody2D;
        private Animator _mAnimator;
        private TriggerCheck2D _mGroundTriggerCheck2D;
        private TriggerCheck2D _mCeilingTriggerCheck2D;
        private IGunController _mGunController;

        private ExpiableProperty<bool> _mJumpDown;
        [SerializeField]
        private bool _mCrouchPressed;
        private bool _mFirePressed;
        
        [SerializeField]
        private float _mCrouchTimer = 0f;
        private static readonly int Crouching = Animator.StringToHash("Crouching");

        private void Awake()
        {
            _mRigidbody2D = GetComponent<Rigidbody2D>();
            _mAnimator = GetComponentInChildren<Animator>();
            
            _mGunController = mGun.GetComponent<BulletGunController>();
            _mGroundTriggerCheck2D = mGroundCheck.GetComponent<TriggerCheck2D>();
            _mCeilingTriggerCheck2D = mCeilingCheck.GetComponent<TriggerCheck2D>();

            _mJumpDown = new(operationTimeout);
            _mCrouchPressed = false;
            _mFirePressed = false;
            _mCrouchTimer = 0f;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _mJumpDown.Value = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _mFirePressed = true;
            }
            
            if (Input.GetButtonDown("Crouch"))
            {
                _mCrouchPressed = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                _mCrouchPressed = false;
            }
            
            if (_mFirePressed && _mGunController.CanFire())
            {
                _mFirePressed = false;
                _mGunController.Fire();
            }
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
        }

        public IArchitecture GetArchitecture()
        {
            return ProgenyPlatform.Interface;
        }
    }
}
