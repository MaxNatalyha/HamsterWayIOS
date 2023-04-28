using GameUI;
using LevelGenerator;
using MatchingCards;
using Pet;
using Pipeline;
using Services;
using Services.Ads;
using SettingsScripts;
using Shop;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class InitializeState : IGlobalState
    {
        private ILoadingPage _loadingPage;
        private IShopController _shopController;
        private ISkinsService _skinsService;
        private IAudioService _audioService;
        private IIAPService _iapService;
        private IAdsService _adsService;
        private ILevelObjectsPool _levelObjectsPool;
        private IPipelineGame _pipelineGame;
        private IMatchingCardsGame _matchingCardsGame;
        private IPetController _petController;
        
        private readonly ICustomLogger _logger;
        private readonly GlobalStateMachine _stateMachine;
        
        public InitializeState(GlobalStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(
            ILoadingPage loadingPage,
            IShopController shopController,
            ISkinsService skinsService,
            IAudioService audioService,
            IIAPService iapService,
            IAdsService adsService,
            ILevelObjectsPool levelObjectsPool,
            IPipelineGame pipelineGame,
            IMatchingCardsGame matchingCardsGame,
            IPetController petController)
        {
            _loadingPage = loadingPage;
            _shopController = shopController;
            _skinsService = skinsService;
            _audioService = audioService;
            _iapService = iapService;
            _adsService = adsService;
            _levelObjectsPool = levelObjectsPool;
            _pipelineGame = pipelineGame;
            _matchingCardsGame = matchingCardsGame;
            _petController = petController;
        }

        public void Enter()
        {
            _logger.PrintInfo("Global Initialize State", "Enter");
            _loadingPage.UpdateProgress("Initialize", 0.5f);
            
            Settings.Initialize();
            
            InitializeServices();
            InitializeGames();
            InitializeShopController();
            
            _stateMachine.EnterState<BuildUIState>();
        }

        private void InitializeShopController()
        {
            _shopController.Initialize();
        }

        private void InitializeServices()
        {
            _skinsService.Initialize();
            _audioService.Initialize();
            _iapService.Initialize();
            _adsService.Initialize();
        }

        private void InitializeGames()
        {
            _levelObjectsPool.Initialize();
            _pipelineGame.Initialize();
            _matchingCardsGame.Initialize();
            _petController.Initialize();
        }
    }
}