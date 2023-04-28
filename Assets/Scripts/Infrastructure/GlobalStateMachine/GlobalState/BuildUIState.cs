using GameUI;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class BuildUIState : IGlobalState
    {
        private ILoadingPage _loadingPage;
        private IUIBuilder _uiBuilder;
        
        private readonly GlobalStateMachine _stateMachine;
        private readonly ICustomLogger _logger;
        
        public BuildUIState(GlobalStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(ILoadingPage loadingPage, IUIBuilder uiBuilder)
        {
            _loadingPage = loadingPage;
            _uiBuilder = uiBuilder;
        }
        
        public void Enter()
        {
            _logger.PrintInfo("Global Build UI State", "Enter");
            _loadingPage.UpdateProgress("Строим UI", 0.75f);
            
            _uiBuilder.Build();
            
            _stateMachine.EnterState<GameState>();
        }
    }
}