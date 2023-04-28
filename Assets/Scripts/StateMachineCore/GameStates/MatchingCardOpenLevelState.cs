using GameUI;
using LevelGenerator;
using MatchingCards;
using SaveSystem;
using Services;
using Utilities;

namespace StateMachineCore.GameStates
{
    public class MatchingCardOpenLevelState : IStateWithArg<MatchingCardsLevelData>
    {
        private readonly MatchingCardsLevelPage _levelPage;
        private readonly IMatchingCardLevelGenerator _levelGenerator;
        private readonly IPageNavigationService _pageNavigationService;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ICustomLogger _logger;
        private readonly ILevelPopup _levelPopup;
        private readonly StateMachine _stateMachine;

        public MatchingCardOpenLevelState(
            MatchingCardsLevelPage levelPage, 
            IMatchingCardLevelGenerator levelGenerator, 
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
            _logger = new DebugLogger();
        }

        public void Enter(MatchingCardsLevelData levelData)
        {
            _logger.PrintInfo("Open Level State", "Enter");

            _levelGenerator.CleanUp();
            _levelGenerator.Generate(levelData);
            
            _levelPopup.Close();
            
            _levelPage.upperPanel.TimeBar.SetRewardData(levelData.rewardData);
            _levelPage.upperPanel.LevelText.text = (levelData.LevelID + 1).ToString();
            
            _levelPage.bottomPanel.ClickAmount.text = levelData.clicksAmount.ToString();

            _playerProgressService.SetGameLevelProgress(levelData, 0);
            _pageNavigationService.OpenPage(PagesEnum.MatchingCardsLevel).ClosePage(levelData.GameType);
            
            _stateMachine.EnterState<IdleState>();
        }

        public void Exit()
        {
            _logger.PrintInfo("Open Level State", "Exit");
        }
    }
}