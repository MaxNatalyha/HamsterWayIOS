using System;
using System.Collections.Generic;
using GameUI;
using LevelGenerator;
using SaveSystem;
using Services;
using StateMachineCore;
using StateMachineCore.GameStates;
using Zenject;

namespace Pipeline
{
    public class PipelineGame : IPipelineGame
    {
        private PipelineLevelData _currentOpenLevel;
        
        private IPipelineLevelGenerator _levelGenerator;
        private ILevelsProvider _levelsProvider;
        private ILevelPopup _levelPopup;

        private IRewardService _rewardService;
        private IPageNavigationService _pageNavigation;
        private IPlayerProgressService _playerProgress;
        private IPipelineObjectsProvider _objectsProvider;

        private readonly PipelineGameLevelPage _levelPage;
        private readonly StateMachine _stateMachine;

        public PipelineGame(PipelineGameLevelPage levelPage)
        {
            _levelPage = levelPage;
            _stateMachine = new StateMachine();
        }

        [Inject]
        public void Construct( 
            ILevelsProvider levelsProvider,
            IPipelineLevelGenerator levelGenerator,
            IPageNavigationService pageNavigation,
            IRewardService rewardService,
            IPlayerProgressService playerProgress,
            ILocalizationService localizationService,
            IPipelineObjectsProvider objectsProvider)
        {
            _levelsProvider = levelsProvider;
            _levelGenerator = levelGenerator;
            _pageNavigation = pageNavigation;
            _rewardService = rewardService;
            _playerProgress = playerProgress;
            _objectsProvider = objectsProvider;
            _levelPopup = new LevelPopup(localizationService);
        }

        public void Initialize()
        {
            _levelPage.upperPanel.TimeBar.Initialize();
            
            _levelPopup.Initialize(_levelPage.levelPopupView);
            
            InitializeStateMachine();
            
            _levelGenerator.Initialize(_levelPage.levelView.LevelObjectsContainer, _levelPage.levelView.LevelUpperLayerContainer);
            
            BindUI();
            BindLevelObjects();
        }

        private void InitializeStateMachine()
        {
            var states = new Dictionary<Type, IExitableState>
            {
                [typeof(PipelineOpenLevelState)] = new PipelineOpenLevelState(_levelPage, _levelGenerator, _pageNavigation, _playerProgress, _stateMachine, _levelPopup),
                [typeof(PauseState)] = new PauseState(_levelPage.upperPanel, _levelPopup),
                [typeof(IdleState)] = new IdleState(),
                [typeof(HamsterRunningState)] = new HamsterRunningState(_levelPage, _objectsProvider),
                [typeof(LoseState)] = new LoseState(_levelPage.upperPanel, _levelPopup),
                [typeof(WinState)] = new WinState(_levelPage.upperPanel, _rewardService, _levelPopup),
            };
            
            _stateMachine.SetStates(states);
        }

        public void StartGame(ILevelData level)
        {
            _currentOpenLevel = _levelsProvider.PipelineLevelsDictionary[level.GameType][level.LevelID];
            _stateMachine.EnterState<PipelineOpenLevelState, PipelineLevelData>(_currentOpenLevel);
        }

        private void BackToMenu()
        {
            _pageNavigation.OpenPage(_currentOpenLevel.GameType).ClosePage(PagesEnum.PipelineLevel);
        }

        private void NextLevel()
        {
            int levelIndex = Array.IndexOf(_levelsProvider.PipelineLevelsDictionary[_currentOpenLevel.GameType], _currentOpenLevel);

            if (_levelsProvider.PipelineLevelsDictionary[_currentOpenLevel.GameType].Length <= levelIndex + 1)
                BackToMenu();
            else
                StartGame(_levelsProvider.PipelineLevelsDictionary[_currentOpenLevel.GameType][levelIndex + 1]);
        }

        private void BindLevelObjects()
        {
            _objectsProvider.HamsterHouse.BindAction(
                onClickAction: () => _stateMachine.EnterState<HamsterRunningState>());

            _objectsProvider.Hamster.BindActions(
                onWin: () => _stateMachine.EnterState<WinState, ILevelData>(_currentOpenLevel),
                onLose: () => _stateMachine.EnterState<LoseState, string>(LocalizationKeys.WRONG_WAY_KEY));
        }

        private void BindUI()
        {
            _levelPage.upperPanel.PauseButton.onClick.AddListener(() => _stateMachine.EnterState<PauseState>());
            _levelPage.upperPanel.TimeBar.OnTimeLeftEvent += () => _stateMachine.EnterState<LoseState, string>(LocalizationKeys.TIMEOVER_KEY);

            _levelPage.levelView.SkipHamsterButton.onClick.AddListener(_objectsProvider.Hamster.SkipRun);
            
            _levelPage.bottomPanel.SkipLevel.SetAction(onBuy: NextLevel);
            _levelPage.bottomPanel.ExtraTime.SetAction(onBuy: () => _levelPage.upperPanel.TimeBar.AddExtraTime(100));
            
            _levelPage.levelPopupView.nextLevelButton.onClick.AddListener(NextLevel);
            _levelPage.levelPopupView.continueButton.onClick.AddListener(() => _stateMachine.EnterState<IdleState>());
            _levelPage.levelPopupView.restartButton.onClick.AddListener(() => _stateMachine.EnterState<PipelineOpenLevelState, PipelineLevelData>(_currentOpenLevel));
            _levelPage.levelPopupView.menuButton.onClick.AddListener(BackToMenu);
            
            _levelPopup.AdsButton.SetRewardAdCompleteAction(_rewardService.DoubleReward);
        }
    }
}
