using UnityEngine;

namespace StateMachine.PlayerState
{
    [CreateAssetMenu(menuName = "Data/StateMachine/PlayerStateBase/Aim", fileName = "PlayerAimState")]
    public class PlayerAimState : PlayerStateBase
    {
        [SerializeField] private float aimProgress;
        [SerializeField] private float aimDuration = 0.5f;
        
        public override void Enter()
        {
            base.Enter();
            aimProgress = 0f;
        }

        public override void Exit()
        {
            base.Exit();
            aimProgress = 0f;
        }

        public override void Update()
        {
            
        }
        
        public override void FixUpdate()
        {
            base.FixUpdate();
            aimProgress += Time.fixedDeltaTime;
            if (aimProgress >= aimDuration)
            {
                // mStateMachine.SwitchState(typeof(PlayerShootState));
            }
        }
    }
}