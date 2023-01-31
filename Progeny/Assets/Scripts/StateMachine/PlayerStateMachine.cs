using Controller;
using StateMachine.PlayerState;
using UnityEngine;

namespace StateMachine
{
    public class PlayerStateMachine : Interface.StateMachine
    {
        [SerializeField] private PlayerStateBase[] states;
        [SerializeField] private Animator mAnimator;
        [SerializeField] private PlayerController mPlayerController;
        
        private void Awake()
        {
            if (mAnimator == null)
                mAnimator = GetComponentInChildren<Animator>();

            if (mPlayerController == null)
                mPlayerController = GetComponent<PlayerController>();
            
            foreach (var state in states)
            {
                state.Initialize(this, mAnimator, mPlayerController);
                stateTable.Add(state.GetType(), state);
            }
        }
        
        private void Start()
        {
            currentState = null;
            SwitchState<PlayerIdleState>();
        }
    }
}