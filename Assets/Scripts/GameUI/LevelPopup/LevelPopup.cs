using System;
using System.Collections.Generic;
using Services;
using Services.Ads;
using StateMachineCore;

namespace GameUI
{
    public class LevelPopup : ILevelPopup
    {
        public RewardAdButton AdsButton => _view.adsButton;
        
        private LevelPopupView _view;

        private readonly ILocalizationService _localizationService;
        private readonly StateMachine _viewStateMachine;
        
        public LevelPopup(ILocalizationService localizationService)
        {
            _viewStateMachine = new StateMachine();
            _localizationService = localizationService;
        }

        public void Initialize(LevelPopupView view)
        {
            _view = view;

            var states = new Dictionary<Type, IExitableState>()
            {
                [typeof(PopupPauseState)] = new PopupPauseState(view),
                [typeof(PopupLoseState)] = new PopupLoseState(view, 700, 1100),
                [typeof(PopupWinState)] = new PopupWinState(view)
            };
            
            _viewStateMachine.SetStates(states);
        }

        public void Pause()
        {
            _viewStateMachine.EnterState<PopupPauseState>();
            _view.gameObject.SetActive(true);
        }

        public void Lose(string loseReasonKey)
        {
            string localize = _localizationService.GetLocalizedText(loseReasonKey);
            
            _viewStateMachine.EnterState<PopupLoseState, string>(localize);
            _view.gameObject.SetActive(true);
        }

        public void Win(int reward, int progress)
        {
            _viewStateMachine.EnterState<PopupWinState>();
            _view.gameObject.SetActive(true);
            _view.StartDisplayStarsRoutine(reward, progress);
        }

        public void ShowDoubledReward(int reward)
        {
            _view.rewardField.UpdateField(reward);
        }

        public void Close()
        {
            _view.gameObject.SetActive(false);
        }
    }
}
