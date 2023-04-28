using System;
using System.Collections.Generic;

namespace StateMachineCore
{
    public class StateMachine
    {
        private IExitableState _currentState;
        private Dictionary<Type, IExitableState> _states;

        public void SetStates(Dictionary<Type, IExitableState> states)
        {
            _states = states;
        }
        
        public void EnterState<TState>() where TState : IState
        {
            _currentState?.Exit();

            var state = _states[typeof(TState)];
            (state as IState)?.Enter();
            
            _currentState = state;
        }

        public void EnterState<TState, TArgs>(TArgs args) where TState : IExitableState
        {
            _currentState?.Exit();
            
            var state = _states[typeof(TState)];
            (state as IStateWithArg<TArgs>)?.Enter(args);
            
            _currentState = state;
        }
    }
}