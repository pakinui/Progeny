using UnityEngine;

namespace StateMachine.PlayerState
{
    [CreateAssetMenu(menuName = "Data/StateMachine/PlayerStateBase/Hurt", fileName = "PlayerHurtState")]
    public class PlayerHurtState : PlayerStateBase
    {
        [SerializeField] private float hurtDuration = 0.5f;
        [SerializeField] private float hurtProgress;
        [SerializeField] private Vector2 hurtForce = new Vector2(10f, 10f);
        
        public override void Update()
        {
            base.Update();
            if (hurtProgress >= hurtDuration)
            {
                mStateMachine.SwitchState<PlayerIdleState>();
            }
        }
        
        public override void FixUpdate()
        {
            base.FixUpdate();
            hurtProgress += Time.fixedDeltaTime;
        }
        
        public override void Enter()
        {
            base.Enter();
            hurtProgress = 0f;
            mPlayerController.Velocity = Vector2.zero;
            // TODO need to check if the player is facing right or left
            // mPlayerController.SetForce(hurtForce *  ? Vector2.right : Vector2.left), ForceMode2D.Impulse);
        }
    }
}