using GameUI;
using Utilities;

namespace StateMachineCore.GameStates
{
    public class LoseState : IStateWithArg<string>
    {
        private readonly LevelPageUpperPanel _upperPanel;
        private readonly ILevelPopup _levelPopup;
        private readonly ICustomLogger _logger;

        public LoseState(LevelPageUpperPanel upperPanel, ILevelPopup levelPopup)
        {
            _upperPanel = upperPanel;
            _levelPopup = levelPopup;
            _logger = new DebugLogger();
        }

        public virtual void Enter(string loseReason)
        {
            _logger.PrintInfo("Lose State", "Enter");
            
            _upperPanel.TimeBar.Pause();
            _levelPopup.Lose(loseReason);
        }

        public void Exit()
        {
            _logger.PrintInfo("Lose State", "Exit");
        }
    }
}