using System;
using System.Collections.Generic;
using GameUI;
using LevelGenerator;
using Pipeline;
using SaveSystem;
using Services;
using StateMachineCore;
using StateMachineCore.GameStates;
using Utilities;
using Zenject;

namespace MatchingCards
{
    public class MatchingCardsGame : IMatchingCardsGame
    {
        private MatchingCardsLevelData _currentOpenLevel;

        private Card _lastOpedCard;
        private int _openCardsPairs;
        private int _currentLevelClickAmount;
        
        private IMatchingCardLevelGenerator _levelGenerator;
        private ILevelsProvider _levelsProvider;
        private ICardsProvider _cardsProvider;
        private ILevelPopup _levelPopup;

        private IAudioService _audioService;
        private IRewardService _rewardService;
        private IParticleService _particleService;
        private IPlayerProgressService _playerProgress;
        private IPageNavigationService _pageNavigationService;

        private readonly StateMachine _stateMachine;
        private readonly MatchingCardsLevelPage _levelPage;
        private readonly ICustomLogger _logger;

        public MatchingCardsGame(MatchingCardsLevelPage levelPage)
        {
            _levelPage = levelPage;
            _stateMachine = new StateMachine();
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IAudioService audioService, 
            IParticleService particleService, 
            IPageNavigationService pageNavigationService, 
            IRewardService rewardService, 
            ILevelsProvider levelsProvider,
            IMatchingCardLevelGenerator levelGenerator,
            IPlayerProgressService playerProgress,
            ILocalizationService localizationService,
            ICardsProvider cardsProvider)
        {
            _levelsProvider = levelsProvider;
            _audioService = audioService;
            _particleService = particleService;
            _pageNavigationService = pageNavigationService;
            _rewardService = rewardService;
            _levelGenerator = levelGenerator;
            _playerProgress = playerProgress;
            _cardsProvider = cardsProvider;

            _levelPopup = new LevelPopup(localizationService);
        }

        public void Initialize()
        {
            _levelPage.upperPanel.TimeBar.Initialize();
            
            _levelPopup.Initialize(_levelPage.levelPopupView);
            
            InitializeStateMachine();
            
            _levelGenerator.Initialize(_levelPage.cardsContainer, OnCardOpen);
            
            BindUI();
        }

        public void StartGame(ILevelData level)
        {
            _currentOpenLevel = _levelsProvider.MatchingCardsLevels[level.LevelID];
            _currentLevelClickAmount = _currentOpenLevel.clicksAmount;
            _openCardsPairs = 0;
            _stateMachine.EnterState<MatchingCardOpenLevelState, MatchingCardsLevelData>(_currentOpenLevel);
        }

        private void OnCardOpen(Card card)
        {
            _currentLevelClickAmount--;
            _levelPage.bottomPanel.ClickAmount.text = _currentLevelClickAmount.ToString();
            
            if (_currentLevelClickAmount <= 0)
                _stateMachine.EnterState<CardsLoseState, string>(LocalizationKeys.CLICKS_OVER_KEY);
        
            if (_lastOpedCard == null)
            {
                _lastOpedCard = card;
                return;
            }
            
            if (_lastOpedCard.Id == card.Id)
                Match(card);
            else if (_lastOpedCard.Id != card.Id)
                NotMatch(card);
        }

        private void NotMatch(Card card)
        {
            _logger.PrintInfo("Matching Cards", "Did not match");

            card.CloseCard();
            _lastOpedCard.CloseCard();

            _lastOpedCard = null;
        }

        private void Match(Card card)
        {
            _logger.PrintInfo("Matching Cards", "Match");

            _lastOpedCard.IsMatch = true;
            card.IsMatch = true;

            _particleService.PlayStars(_lastOpedCard.transform.position, card.transform.position);
            _audioService.PlaySound(AudioTypes.StarReward);
            
            _openCardsPairs++;

            if (_openCardsPairs == _cardsProvider.CardsPairs)
                _stateMachine.EnterState<CardsWinState, ILevelData>(_currentOpenLevel);
            

            _lastOpedCard = null;
        }

        private void BackToMenu()
        {
            _pageNavigationService.OpenPage(PagesEnum.MatchingCardsSelection).ClosePage(PagesEnum.MatchingCardsLevel);
        }

        private void NextLevel()
        {
            int levelIndex = Array.IndexOf(_levelsProvider.MatchingCardsLevels, _currentOpenLevel);
            
            if (_levelsProvider.MatchingCardsLevels.Length <= levelIndex + 1)
                BackToMenu();
            else
                StartGame(_levelsProvider.MatchingCardsLevels[levelIndex + 1]);
        }
        
        private void AddExtraClicks()
        {
            _currentLevelClickAmount += 50;
            _levelPage.bottomPanel.ClickAmount.text = _currentLevelClickAmount.ToString();
        }

        private void InitializeStateMachine()
        {
            var states = new Dictionary<Type, IExitableState>
            {
                [typeof(MatchingCardOpenLevelState)] = new MatchingCardOpenLevelState(_levelPage, _levelGenerator, _pageNavigationService, _playerProgress, _stateMachine, _levelPopup),
                [typeof(IdleState)] = new IdleState(),
                [typeof(PauseState)] = new PauseState(_levelPage.upperPanel, _levelPopup),
                [typeof(CardsLoseState)] = new CardsLoseState(_levelPage.upperPanel, _cardsProvider, _levelPopup),
                [typeof(CardsWinState)] = new CardsWinState(_levelPage.upperPanel, _rewardService, _cardsProvider, _levelPopup)
            };
            
            _stateMachine.SetStates(states);        
        }

        private void BindUI()
        {
            _levelPage.upperPanel.PauseButton.onClick.AddListener(() => _stateMachine.EnterState<PauseState>());
            _levelPage.upperPanel.TimeBar.OnTimeLeftEvent += () => _stateMachine.EnterState<CardsLoseState, string>(LocalizationKeys.TIMEOVER_KEY);
            
            _levelPage.bottomPanel.ExtraClick.SetAction(onBuy: AddExtraClicks);
            _levelPage.bottomPanel.ExtraTime.SetAction(onBuy: () => _levelPage.upperPanel.TimeBar.AddExtraTime(100));
            
            _levelPage.levelPopupView.nextLevelButton.onClick.AddListener(NextLevel);
            _levelPage.levelPopupView.continueButton.onClick.AddListener(() => _stateMachine.EnterState<IdleState>());
            _levelPage.levelPopupView.restartButton.onClick.AddListener(() => StartGame(_currentOpenLevel));
            _levelPage.levelPopupView.menuButton.onClick.AddListener(BackToMenu);
            
            _levelPopup.AdsButton.SetRewardAdCompleteAction(_rewardService.DoubleReward);
        }
    }
}
