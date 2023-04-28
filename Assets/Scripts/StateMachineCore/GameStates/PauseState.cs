using GameUI;
using Utilities;

namespace StateMachineCore.GameStates
{
    public class PauseState : IState
    {
        private readonly LevelPageUpperPanel _upperPanel;
        private readonly ICustomLogger _customLogger;
        private readonly ILevelPopup _levelPopup;

        public PauseState(LevelPageUpperPanel upperPanel, ILevelPopup levelPopup)
        {
            _upperPanel = upperPanel;
            _levelPopup = levelPopup;
            _customLogger = new DebugLogger();
        }
        
        public void Enter()
        {
            _customLogger.PrintInfo("Pause State", "Enter");

            _upperPanel.TimeBar.Pause();
            _levelPopup.Pause();
        }

        public void Exit()
        {
            _customLogger.PrintInfo("Pause State", "Exit");
            _upperPanel.TimeBar.Resume();
            _levelPopup.Close();
        }
    }
}