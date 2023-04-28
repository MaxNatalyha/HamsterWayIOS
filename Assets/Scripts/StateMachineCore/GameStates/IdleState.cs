using Utilities;

namespace StateMachineCore.GameStates
{
    public class IdleState : IState
    {
        private readonly ICustomLogger _customLogger;

        public IdleState()
        {
            _customLogger = new DebugLogger();
        }
        
        public void Enter()
        {
            _customLogger.PrintInfo("Idle State", "Enter");
            
        }

        public void Exit()
        {
            _customLogger.PrintInfo("Idle State", "Exit");
        }
    }
}