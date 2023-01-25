using Framework;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour, IController
    {
        public GameObject mGun;
        public GameObject mGroundCheck;
        public GameObject mCeilingCheck;
    
        public float operationTimeout = 0.2f;
    
        private Rigidbody2D _mRigidbody2D;
        private TriggerCheck2D _mGroundTriggerCheck2D;
        private TriggerCheck2D _mCeilingTriggerCheck2D;
        
        private IGunController _mGunController;

        private ExpiableProperty<bool> _mJumpPressed;
        private ExpiableProperty<bool> _mCrouchPressed;
        private ExpiableProperty<bool> _mFirePressed;

        private void Awake()
        {
            _mRigidbody2D = GetComponent<Rigidbody2D>();
            _mGroundTriggerCheck2D = mGroundCheck.GetComponent<TriggerCheck2D>();
            // mCeilingTriggerCheck2D = mCeilingCheck.GetComponent<TriggerCheck2D>();
            
            _mGunController = mGun.GetComponent<BulletGunController>();
        
            _mJumpPressed = new(operationTimeout);
            _mCrouchPressed = new(operationTimeout);
            _mFirePressed = new(operationTimeout);
            
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _mJumpPressed.Value = true;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                _mFirePressed.Value = true;
            }
             if (Input.GetButtonDown("Crouch"))
            {
                _mCrouchPressed.Value = true;
            }
        
            if (_mFirePressed.Value && _mGunController.CanFire())
            {
                _mFirePressed.Value = false;
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
            
            
            if (_mJumpPressed.Value && _mGroundTriggerCheck2D.Triggered())
            {
                _mJumpPressed.Value = false;
                // mRigidbody2D.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                _mRigidbody2D.velocity = new Vector2(_mRigidbody2D.velocity.x, 5);
            }
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingPlatform.Interface;
        }
    }
}
