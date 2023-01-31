using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void FixUpdate();
    }
    
    public interface IStateMachine
    {
        void SwitchState(IState nextState);
        void SwitchState<T>() where T : IState;
        void Update();
        void FixedUpdate();
    }
    
    public class StateMachine : MonoBehaviour, IStateMachine
    {
        protected IState currentState;
        protected readonly Dictionary<Type, IState> stateTable = new Dictionary<Type, IState>();
        
        public void SwitchState(IState nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState?.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            SwitchState(stateTable[typeof(T)]);
        }
        
        public void Update()
        {
            currentState.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixUpdate();
        }
    }
}