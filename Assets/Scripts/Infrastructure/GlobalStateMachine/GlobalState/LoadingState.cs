using System.Collections.Generic;
using GameUI;
using LevelGenerator;
using Pet;
using Pipeline;
using SaveSystem;
using Services;
using Shop;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class LoadingState : IGlobalState
    {
        private ILoadingPage _loadingPage;
        private IPetController _petController;
        private ILocalizationService _localizationService;
        private ILevelObjectsPool _levelObjectsPool;
        private IUIBuilder _uiBuilder;
        private IPetFoodProvider _petFoodProvider;
        private IShopController _shopController;
        private IPlayerProgressService _progressService;
        private ISkinsService _skinsService;
        private ILevelsProvider _levelsProvider;
        private PetFoodFactory _petFoodFactory;
        
        private readonly GlobalStateMachine _stateMachine;
        private readonly ICustomLogger _logger;
        
        public LoadingState(GlobalStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(
            ILoadingPage loadingPage,
            IPetController petController,
            ILocalizationService localizationService,
            ILevelObjectsPool levelObjectsPool,
            IUIBuilder uiBuilder,
            IPetFoodProvider petFoodProvider,
            IPlayerProgressService progressService,
            ISkinsService skinsService,
            ILevelsProvider levelsProvider,
            IShopController shopController,
            PetFoodFactory petFoodFactory)
        {
            _loadingPage = loadingPage;
            _petController = petController;
            _localizationService = localizationService;
            _levelObjectsPool = levelObjectsPool;
            _uiBuilder = uiBuilder;
            _petFoodProvider = petFoodProvider;
            _progressService = progressService;
            _skinsService = skinsService;
            _levelsProvider = levelsProvider;
            _shopController = shopController;
            _petFoodFactory = petFoodFactory;
        }

        public async void Enter()
        {
            _logger.PrintInfo("Global Loading State", "Enter");
            _loadingPage.UpdateProgress("Loading", 0.25f);

            _localizationService.Load();
            await _levelObjectsPool.Load();
            
            LoadPlayerData();
            LoadSkinsData();
            LoadLevels();
            
            _petFoodProvider.SetFood(_petFoodFactory.CreatePetFood());

            await _uiBuilder.Load();
            await _shopController.Load();
            await _petController.Load();

            _stateMachine.EnterState<InitializeState>();
        }
        private void LoadPlayerData()
        {
            var playerDataLoader = new PlayerDataLoader();
            var loadedData = playerDataLoader.Load();
            
            _progressService.SetPlayerData(loadedData);
        }        
        
        private void LoadSkinsData()
        {
            var skinsDataLoader = new SkinsDataLoader();
            var loadedData = skinsDataLoader.Load();
            
            _skinsService.Load(loadedData);
        }

        private void LoadLevels()
        {
            ILevelLoader levelLoader = new ResourceLevelLoader();
            
            var pipelineLevelsDictionary = new Dictionary<GamesTypes, PipelineLevelData[]>
            {
                { GamesTypes.EasyPipeline, levelLoader.LoadPipelineLevelsByType(GamesTypes.EasyPipeline) },
                { GamesTypes.ClassicPipeline, levelLoader.LoadPipelineLevelsByType(GamesTypes.ClassicPipeline) }
            };

            var matchingCardLevels = levelLoader.LoadMatchingCardsLevels();
            
            _levelsProvider.SetLevels(pipelineLevelsDictionary, matchingCardLevels);
        }
    }
}