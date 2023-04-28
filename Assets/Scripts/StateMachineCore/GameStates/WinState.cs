using GameUI;
using SaveSystem;
using Services;
using Utilities;

namespace StateMachineCore.GameStates
{
    public class WinState : IStateWithArg<ILevelData>
    {
        private readonly LevelPageUpperPanel _upperPanel;
        private readonly IRewardService _rewardService;
        private readonly ICustomLogger _customLogger;
        private readonly ILevelPopup _levelPopup;
        
        public WinState(LevelPageUpperPanel upperPanel, IRewardService rewardService, ILevelPopup levelPopup)
        {
            _upperPanel = upperPanel;
            _rewardService = rewardService;
            _levelPopup = levelPopup;
            _customLogger = new DebugLogger();
        }

        public virtual void Enter(ILevelData level)
        {
            _customLogger.PrintInfo("Win State", "Enter");
            
            int starAmount = _upperPanel.TimeBar.ActiveStarsAmount;
            
            _upperPanel.TimeBar.Pause();
            _rewardService.AddReward(starAmount, level, _levelPopup);        
        }

        public void Exit()
        {
            _customLogger.PrintInfo("Win State", "Exit");
        }
    }
}