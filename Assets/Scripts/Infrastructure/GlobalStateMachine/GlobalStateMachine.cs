using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GlobalStateMachine
    {
        private IGlobalState _currentState;
        private Dictionary<Type, IGlobalState> _states;

        public void SetStates(Dictionary<Type, IGlobalState> states)
        {
            _states = states;
        }

        public void EnterState<TState>() where TState : IGlobalState
        {
            var state = _states[typeof(TState)];
            state.Enter();
        }
    }
}