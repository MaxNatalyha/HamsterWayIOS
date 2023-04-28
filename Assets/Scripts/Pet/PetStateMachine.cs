using System.Collections.Generic;

namespace Pet
{
    public class PetStateMachine
    {
        private PetState _currentState;
        private List<PetState> _states;
        private PetView _view;

        public void Initialize(List<PetState> states, PetView view)
        {
            _states = states;
            _view = view;
        }
        
        public void UpdateState(float satiety)
        {
            if(_currentState?.satietyEnter < satiety && _currentState?.satietyExit > satiety) return;
            if(!_states.Exists(s => satiety >= s.satietyEnter && satiety <= s.satietyExit)) return;
            
            NextState(satiety);
        }

        private void NextState(float satiety)
        {
            var state = _states.Find(s => satiety >= s.satietyEnter && satiety <= s.satietyExit);

            _currentState = state;
            _view.PlayAnimationState(state.state);
        }
    }
}