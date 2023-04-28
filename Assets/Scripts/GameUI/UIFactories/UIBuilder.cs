using Cysharp.Threading.Tasks;
using MatchingCards;
using Pet;
using Pipeline;
using SaveSystem;
using Services;
using Shop;
using Zenject;

namespace GameUI
{
    public class UIBuilder : IUIBuilder
    {
        private IPagesProvider _pagesProvider;
        private ILevelsProvider _levelsProvider;
        
        private readonly PipelineLevelsButtonsFactory _pipelineFactory;
        private readonly MatchingCardsLevelsButtonsFactory _matchingCardsFactory;
        private readonly FoodShopFactory _foodShopFactory;
        
        private const int PIPELINE_LEVELS_PER_PAGE = 24;

        public UIBuilder(
            PipelineLevelsButtonsFactory pipelineFactory,
            MatchingCardsLevelsButtonsFactory matchingCardsFactory, 
            FoodShopFactory foodShopFactory
            )
        {
            _pipelineFactory = pipelineFactory;
            _matchingCardsFactory = matchingCardsFactory;
            _foodShopFactory = foodShopFactory;
        }
        
        [Inject]
        public void Construct(ILevelsProvider levelsProvider, IPagesProvider pagesProvider)
        {
            _pagesProvider = pagesProvider;
            _levelsProvider = levelsProvider;
        }

        public async UniTask Load()
        {
            await _pipelineFactory.Load();
            await _matchingCardsFactory.Load();
            await _foodShopFactory.Load();
        }

        public void Build()
        {
            BuildPipelineSelectionsPages();
            BuildMatchingCardSelectionPage();
            BuildShopFoodSection();
        }

        private void BuildPipelineSelectionsPages()
        {
            var easySelectionPage = _pagesProvider.GetPage<PipelineLevelSelectionPage>(PagesEnum.PipelineEasySelection);
            var classicSelectionPage = _pagesProvider.GetPage<PipelineLevelSelectionPage>(PagesEnum.PipelineClassicSelection);

            CreatePipelineLevelButtonsPages(_levelsProvider.PipelineLevelsDictionary[GamesTypes.EasyPipeline], easySelectionPage);
            CreatePipelineLevelButtonsPages(_levelsProvider.PipelineLevelsDictionary[GamesTypes.ClassicPipeline], classicSelectionPage);
        }

        private void CreatePipelineLevelButtonsPages(PipelineLevelData[] levelsData, PipelineLevelSelectionPage selectionPage)
        {
            var levelButtonsPages = _pipelineFactory.Create(levelsData, PIPELINE_LEVELS_PER_PAGE, selectionPage.LevelButtonsContainer);
            selectionPage.SetSelectionButtonsPages(levelButtonsPages);
        }

        private void BuildMatchingCardSelectionPage()
        {
            var matchingCardSelectionPage = _pagesProvider.GetPage<MatchingCardLevelSelectionPage>(PagesEnum.MatchingCardsSelection);
            var selectionButtons = _matchingCardsFactory.Create(_levelsProvider.MatchingCardsLevels, matchingCardSelectionPage.LevelButtonsContainer);
            
            matchingCardSelectionPage.SetSelectionButtons(selectionButtons);
        }

        private void BuildShopFoodSection()
        {
            var shopPage = _pagesProvider.GetPage<ShopPage>(PagesEnum.Shop);
            _foodShopFactory.Create(shopPage.petFoodSection.foodContainer);
        }
    }
}