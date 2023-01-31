using Controller;
using Interface;
using UnityEngine;

namespace StateMachine
{
    public class PlayerStateBase : ScriptableObject, IState
    {
        [SerializeField] public new string name;
        [SerializeField] public float transitionDuration = 0.1f;
        
        protected IStateMachine mStateMachine;
        protected PlayerController mPlayerController;
        private Animator mAnimator;
        private int animatorHash;
        private float stateStartTime;
        protected float StateDuration => Time.time - stateStartTime;
        protected float StateLength => mAnimator.GetCurrentAnimatorStateInfo(0).length;
        private void OnEnable()
        {
            // state hash
            animatorHash = Animator.StringToHash(name);
        }
        
        public void Initialize(PlayerStateMachine stateMachine, Animator animator, PlayerController playerController)
        {
            this.mStateMachine = stateMachine;
            this.mAnimator = animator;
            this.mPlayerController = playerController;
        }

        public virtual void Enter()
        {
            mAnimator.CrossFade(animatorHash, 0.1f);
            stateStartTime = Time.time;
        }

        public virtual void Exit() { }

        public virtual void Update()
        {
            
        }

        public virtual void FixUpdate()
        {   
            
        }
    }
}