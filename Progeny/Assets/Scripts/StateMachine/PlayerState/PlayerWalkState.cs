using UnityEngine;

namespace StateMachine.PlayerState
{
    /*
     * Walk state: it is the state when the player is walking
     * the following are the transitions:
     * - idle
     * - hurt
     * - crouch
     * - attack
     * - aim
     * - climb
     * - die
     */
    [CreateAssetMenu(menuName = "Data/StateMachine/PlayerStateBase/Walk", fileName = "PlayerWalkState")]
    public class PlayerWalkState : PlayerStateBase
    {
        [SerializeField] private float maxWalkSpeed = 5f;
        [SerializeField] private float minWalkSpeed = 0.1f;
        
        [SerializeField] private float walkAcceleration = 10f;
        [SerializeField] private float walkDeceleration = 10f;

        public override void Update()
        {
            base.Update();
            
            // idle
            if (mPlayerController.Velocity.magnitude < minWalkSpeed)
            {
                mStateMachine.SwitchState<PlayerIdleState>();
            }
            
            // crouch 
            
        }
        
        public override void FixUpdate()
        {
            base.FixUpdate();
            var horizontal = (int) Input.GetAxisRaw("Horizontal");
            
            // accelerate
            if (horizontal != 0)
            {
                // mPlayerController.SetForce(horizontal * walkAcceleration * Vector3.right, ForceMode2D.Force);
                mPlayerController.Velocity += horizontal * walkAcceleration * Vector2.right;
            }
            // decelerate
            else if (mPlayerController.Velocity.magnitude > minWalkSpeed)
            {
                if (mPlayerController.Velocity.magnitude < walkDeceleration)
                {
                    mPlayerController.Velocity = Vector2.zero;
                }
                else
                {
                    mPlayerController.Velocity -= walkDeceleration * mPlayerController.Velocity.normalized;
                }
            }
            
            // clamp speed
            if (mPlayerController.Velocity.magnitude > maxWalkSpeed)
            {
                mPlayerController.Velocity = Vector2.ClampMagnitude(mPlayerController.Velocity, maxWalkSpeed);
            }
        }
    }
}