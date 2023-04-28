using StateMachineCore;
using UnityEngine;

namespace GameUI
{
    public class PopupLoseState : IStateWithArg<string>
    {
        private readonly LevelPopupView _view;
        private readonly int _yPopupSize;
        private readonly int _yPopupBaseSize;

        public PopupLoseState(LevelPopupView view, int yPopupSize, int yPopupBaseSize)
        {
            _view = view;
            _yPopupSize = yPopupSize;
            _yPopupBaseSize = yPopupBaseSize;
        }

        public void Enter(string loseReason)
        {
            _view.content.sizeDelta = new Vector2(_view.content.sizeDelta.x, _yPopupSize);

            _view.loseReasonText.text = loseReason;
            _view.loseReasonText.gameObject.SetActive(true);
            _view.menuButton.gameObject.SetActive(true);
            _view.restartButton.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _view.content.sizeDelta = new Vector2(_view.content.sizeDelta.x, _yPopupBaseSize);

            _view.loseReasonText.gameObject.SetActive(false);
            _view.menuButton.gameObject.SetActive(false);
            _view.restartButton.gameObject.SetActive(false);
        }
    }
}