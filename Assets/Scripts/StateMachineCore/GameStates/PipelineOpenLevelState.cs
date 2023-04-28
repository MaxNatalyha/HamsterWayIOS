using GameUI;
using LevelGenerator;
using Pipeline;
using SaveSystem;
using Services;
using Utilities;

namespace StateMachineCore.GameStates
{
    public class PipelineOpenLevelState : IStateWithArg<PipelineLevelData>
    {
        private readonly PipelineGameLevelPage _levelPage;
        private readonly IPipelineLevelGenerator _levelGenerator;
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ICustomLogger _customLogger;
        private readonly StateMachine _stateMachine;
        private readonly ILevelPopup _levelPopup;

        public PipelineOpenLevelState(
            PipelineGameLevelPage levelPage, 
            IPipelineLevelGenerator levelGenerator, 
            IPageNavigationService pageNavigationService, 
            IPlayerProgressService playerProgressService, 
            StateMachine stateMachine,
            ILevelPopup levelPopup)
        {
            _levelPage = levelPage;
            _levelGenerator = levelGenerator;
            _pageNavigationService = pageNavigationService;
            _playerProgressService = playerProgressService;
            _stateMachine = stateMachine;
            _levelPopup = levelPopup;
            _customLogger = new DebugLogger();
        }

        public void Enter(PipelineLevelData levelData)
        {
            _customLogger.PrintInfo("Open Level State", "Enter");

            _levelGenerator.CleanUp();
            _levelGenerator.Generate(levelData);
            
            _levelPage.upperPanel.TimeBar.SetRewardData(levelData.rewardData);
            _levelPage.upperPanel.LevelText.text = (levelData.LevelID + 1).ToString();
            
            _levelPopup.Close();
            
            _playerProgressService.SetGameLevelProgress(levelData, 0);
            _pageNavigationService.OpenPage(PagesEnum.PipelineLevel).ClosePage(levelData.GameType);
            
            _stateMachine.EnterState<IdleState>();
        }

        public void Exit()
        {
            _customLogger.PrintInfo("Open Level State", "Exit");
        }
    }
}