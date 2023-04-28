using StateMachineCore;

namespace GameUI
{
    public class PopupWinState : IState
    {
        private readonly LevelPopupView _view;

        public PopupWinState(LevelPopupView view)
        {
            _view = view;
        }
        
        public void Enter()
        {
            _view.rewardField.SetField(0);
            _view.rewardBlock.gameObject.SetActive(true);
            _view.adsButton.gameObject.SetActive(true);
            _view.starsBlock.gameObject.SetActive(true);
            _view.menuButton.gameObject.SetActive(true);
            _view.nextLevelButton.gameObject.SetActive(true);
            _view.restartButton.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _view.rewardField.SetField(0);
            _view.rewardBlock.gameObject.SetActive(false);
            _view.adsButton.gameObject.SetActive(false);
            _view.starsBlock.gameObject.SetActive(false);
            _view.menuButton.gameObject.SetActive(false);
            _view.nextLevelButton.gameObject.SetActive(false);
            _view.restartButton.gameObject.SetActive(false);
        }
    }
}