using System;
using Cysharp.Threading.Tasks;
using Pet;
using Services;
using Shop.SkinsSection;
using Utilities;
using Zenject;

namespace Shop
{
    public class ShopController : IShopController
    {
        public event Action<ShopCategoriesEnum> OnCategoryChangeEvent;
        public event Action<ShopSectionsEnum> OnSectionChangeEvent;
        
        private IPlayerProgressService _progressService;
        private IAudioService _audioService;
        private IPageNavigationService _pageNavigation;
        
        private readonly SkinShowcase _skinShowcase;
        private readonly ShopPage _shopPage;
        private readonly ICustomLogger _logger;

        private const int CoinsPerExchange = 1000;

        public ShopController(ShopPage shopPage, SkinShowcase skinShowcase)
        {
            _shopPage = shopPage;
            _skinShowcase = skinShowcase;
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IPlayerProgressService progressService, IAudioService audioService, IPageNavigationService pageNavigation)
        {
            _progressService = progressService;
            _audioService = audioService;
            _pageNavigation = pageNavigation;
        }

        public async UniTask Load()
        {
            await _skinShowcase.Load();
        }
        
        public void Initialize()
        {
            _skinShowcase.Initialize();
            
            BindUI();
        }

        private void BindUI()
        {
            _shopPage.backButton.onClick.AddListener(() => _pageNavigation.OpenPage(PagesEnum.Main).ClosePage(PagesEnum.Shop));
            _shopPage.financeSection.currencyExchange.SetAction(OnExchangeBuy);
            
            foreach (var sectionPanel in _shopPage.sectionPanels)
                sectionPanel.Initialize(this);

            foreach (var categoriesButton in _shopPage.categoriesButtons)
                categoriesButton.Initialize(this);
        }

        public void OpenCategory(ShopCategoriesEnum category)
        {
            OnCategoryChangeEvent?.Invoke(category);
            
            if(category is ShopCategoriesEnum.Finance)
                OpenSection(ShopSectionsEnum.Finance);
            
            _logger.PrintInfo("Shop Controller", $"Open category {category}");
        }

        public void OpenSection(ShopSectionsEnum section)
        {
            OnSectionChangeEvent?.Invoke(section);
            
            _skinShowcase.OnSectionChange(section);
            _shopPage.petFoodSection.OnSectionChange(section);
            _shopPage.financeSection.OnSectionChange(section);
            
            _logger.PrintInfo("Shop Controller", $"Open section {section}");
        }

        public void BuyFood(IPetFood food)
        {
            if (_progressService.SpendCurrency(food.Config.PurchaseInfo.Price, food.Config.PurchaseInfo.Currency))
            {
                food.State.Amount++;
                _audioService.PlaySound(AudioTypes.OpenBox);
            }
        }
        
        private void OnExchangeBuy()
        {
            _progressService.AddCurrency(CoinsPerExchange, CurrencyType.Coins);
        }
    }
}