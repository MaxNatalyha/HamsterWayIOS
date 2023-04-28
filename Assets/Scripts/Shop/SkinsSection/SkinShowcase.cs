using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Services;
using Shop.SkinsSection.SkinViewStates;
using SkinSystem;
using StateMachineCore;
using Utilities;
using Zenject;

namespace Shop.SkinsSection
{
    public class SkinShowcase
    {
        public event Action<int> OnVariantPreviewChangeEvent;
        
        private int _displayedSkinId;
        private List<Skin> _currentDisplayedSkins;
        
        private ISkinSectionView _currentAssetView;
        private readonly Dictionary<ShopSectionsEnum, ISkinSectionView> _skinViewsDictionary;

        private readonly StateMachine _viewStateMachine;
        private readonly SkinShowcaseView _showcaseView;

        private IPlayerProgressService _progressService;
        private IPageNavigationService _pageNavigation;
        private ISkinsService _skinsService;

        private readonly DisplayedSkinInfo _skinInfo;
        private readonly VariantsPreview _variantsPreview;

        private readonly ICustomLogger _logger;

        public SkinShowcase(SkinShowcaseView view)
        {
            _showcaseView = view;
            _skinViewsDictionary = new Dictionary<ShopSectionsEnum, ISkinSectionView>(view.skinViews.Length);
            _logger = new DebugLogger();
            _skinInfo = new DisplayedSkinInfo();
            _viewStateMachine = new StateMachine();
            _variantsPreview = new VariantsPreview(this, view.variantsPool, view.variantsContainer);
        }

        [Inject]
        public void Construct(ISkinsService skinsService, IPlayerProgressService progressService, IPageNavigationService pageNavigation)
        {
            _progressService = progressService;
            _pageNavigation = pageNavigation;
            _skinsService = skinsService;
        }

        public async UniTask Load()
        {
            await _variantsPreview.Load();
        }
        
        public void Initialize()
        {
            _variantsPreview.Initialize();

            var states = new Dictionary<Type, IExitableState>
            {
                [typeof(IAPState)] = new IAPState(_showcaseView),
                [typeof(PurchaseState)] = new PurchaseState(_showcaseView, _progressService, _skinInfo),
                [typeof(SelectState)] = new SelectState(_showcaseView, _skinsService, _skinInfo),
            };
            
            _viewStateMachine.SetStates(states);
            
            foreach (var view in _showcaseView.skinViews)
                _skinViewsDictionary.Add(view.ShopSection, view);

            BindView();
        }

        public void OnSectionChange(ShopSectionsEnum section)
        {
            foreach (var skinView in _skinViewsDictionary.Values)
                skinView.OnSectionChange(section);
            
            if(section is ShopSectionsEnum.Food or ShopSectionsEnum.Finance)
                _showcaseView.gameObject.SetActive(false);
            else
                DisplaySkinCategory(section);
        }

        private void BindView()
        {
            _showcaseView.leftButton.onClick.AddListener(() => SwitchSkin(-1));
            _showcaseView.rightButton.onClick.AddListener(() => SwitchSkin(1));

            _showcaseView.buyButton.onClick.AddListener(ConfirmPurchaseSkin);
            _showcaseView.selectButton.onClick.AddListener(SelectSkin);
        }

        private void DisplaySkinCategory(ShopSectionsEnum section)
        {
            _showcaseView.gameObject.SetActive(true);
            _displayedSkinId = 0;

            _currentAssetView = _skinViewsDictionary[section];
            _currentDisplayedSkins = _skinsService.SkinsDictionary[_currentAssetView.SkinCategory];
            
            DisplaySkin(_currentDisplayedSkins[_displayedSkinId]);
        }

        private void DisplaySkin(ISkin skin)
        {
            _currentAssetView.CleanUp();
            _variantsPreview.CleanPreviewPanel();
            
            _skinInfo.Skin = skin;
            _skinInfo.SkinAssetInfo = skin.BaseAsset;
            _skinInfo.VariantId = skin.SelectedVariantId;

            _currentAssetView.DisplayAssetView(skin.BaseAsset);
            DisplayAssetInfo(skin.BaseAsset);
            
            if(skin.HasColorVariants || skin.HasAssetVariants)
            {
                _variantsPreview.ShowSkinVariants(skin);
                DisplaySkinVariant(skin.SelectedVariantId);
            }
        }

        private void DisplayAssetInfo(ISkinAsset assetInfo)
        {
            if(assetInfo.IAP && !assetInfo.Bought)
                _viewStateMachine.EnterState<IAPState>();
            else if(assetInfo.Bought)
                _viewStateMachine.EnterState<SelectState>();
            else 
                _viewStateMachine.EnterState<PurchaseState>();
        }

        public void DisplaySkinVariant(int id)
        {
            OnVariantPreviewChangeEvent?.Invoke(id);
            _skinInfo.VariantId = id;

            if(_skinInfo.Skin.HasAssetVariants)
                DisplayAssetVariant(id);
            else if (_skinInfo.Skin.HasColorVariants)
                DisplayColorVariant(id);
        }

        private void DisplayColorVariant(int id)
        {
            _skinInfo.SkinAssetInfo = _skinInfo.Skin.ColorVariants[id];

            _currentAssetView.DisplayAssetColor(_skinInfo.Skin.ColorVariants[id].color);
            DisplayAssetInfo(_skinInfo.Skin.ColorVariants[id]);
        }

        private void DisplayAssetVariant(int id)
        {
            var asset = id == 0 ? _skinInfo.Skin.BaseAsset : _skinInfo.Skin.AssetsVariants[id - 1];

            _skinInfo.SkinAssetInfo = asset;
            
            _currentAssetView.DisplayAssetView(asset);
            DisplayAssetInfo(asset);
        }

        private void SwitchSkin(int shift)
        {
            int newId = _displayedSkinId + shift;
            
            if (newId > _currentDisplayedSkins.Count - 1)
                newId = 0;

            if (newId < 0)
                newId = _currentDisplayedSkins.Count - 1;

            _displayedSkinId = newId;
            
            DisplaySkin(_currentDisplayedSkins[_displayedSkinId]);
        }

        private void ConfirmPurchaseSkin()
        {
            _pageNavigation.ConfirmWindow.Open(BuySkin);
        }

        private void BuySkin()
        {
            if (!_progressService.SpendCurrency(_skinInfo.SkinAssetInfo.Price, _skinInfo.SkinAssetInfo.Currency)) return;

            _skinInfo.SkinAssetInfo.Bought = true;
            
            _variantsPreview.RefreshPreview(_skinInfo.Skin);
            DisplayAssetInfo(_skinInfo.SkinAssetInfo);
            
            _logger.PrintInfo("SkinShowcase", $"Buy asset in {_skinInfo.Skin.SkinName} {_skinInfo.Skin.Category}");
        }

        private void SelectSkin()
        {
            if (_skinsService.SelectedSkins[_skinInfo.Skin.Category] != _skinInfo.Skin.SkinName)
            {
                _skinsService.SelectSkin(_skinInfo.Skin.Category, _skinInfo.Skin.SkinName);
                _logger.PrintInfo("SkinShowcase", $"In {_skinInfo.Skin.Category} selected {_skinInfo.Skin.SkinName}");
            }
            
            if(_skinInfo.Skin.SelectedVariantId != _skinInfo.VariantId)
            {
                _skinInfo.Skin.SelectedVariantId = _skinInfo.VariantId;
                _logger.PrintInfo("SkinShowcase", $"In {_skinInfo.Skin.SkinName} {_skinInfo.Skin.Category} selected variant {_skinInfo.VariantId}");
            }
            
            DisplayAssetInfo(_skinInfo.SkinAssetInfo);
        }
    }
}