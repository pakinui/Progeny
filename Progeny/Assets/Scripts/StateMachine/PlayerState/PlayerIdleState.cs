using UnityEngine;

namespace StateMachine.PlayerState
{
    [CreateAssetMenu(menuName = "Data/StateMachine/PlayerStateBase/Idle", fileName = "PlayerIdleState")]
    public class PlayerIdleState : PlayerStateBase
    {
        public override void Enter()
        {
            base.Enter();
            mPlayerController.Velocity = Vector2.zero;
        }
        
        public override void Update()
        {
            base.Update();
            
            // walk
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                mStateMachine.SwitchState<PlayerWalkState>();
                return;
            }
            
            // jump
            // if (Input.GetButtonDown("Jump"))
            // {
            //     mStateMachine.SwitchState(typeof(PlayerJumpState));
            // }
            
            // climb TODO: need to check if there is a ladder or box to climb
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                mStateMachine.SwitchState<PlayerClimbState>();
                return;
            }
            
            //
            
        }
    }
}