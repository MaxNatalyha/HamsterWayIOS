using StateMachineCore;

namespace Shop.SkinsSection.SkinViewStates
{
    public class IAPState : IState
    {
        private readonly SkinShowcaseView _skinShowcaseView;

        public IAPState(SkinShowcaseView skinShowcaseView)
        {
            _skinShowcaseView = skinShowcaseView;
        }

        public void Enter()
        {
            _skinShowcaseView.iapPanel.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _skinShowcaseView.iapPanel.gameObject.SetActive(false);
        }
    }
}