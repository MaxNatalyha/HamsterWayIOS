using StateMachineCore;

namespace GameUI
{
    public class PopupPauseState : IState
    {
        private readonly LevelPopupView _view;

        public PopupPauseState(LevelPopupView view)
        {
            _view = view;
        }
        
        public void Enter()
        {
            _view.menuButton.gameObject.SetActive(true);
            _view.restartButton.gameObject.SetActive(true);
            _view.continueButton.gameObject.SetActive(true);
            _view.settingsBlock.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _view.menuButton.gameObject.SetActive(false);
            _view.restartButton.gameObject.SetActive(false);
            _view.continueButton.gameObject.SetActive(false);
            _view.settingsBlock.gameObject.SetActive(false);
        }
    }
}