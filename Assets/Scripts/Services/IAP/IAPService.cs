using System;
using System.Collections.Generic;
using Services.PurchaseCommands;
using Shop;
using UnityEngine.Purchasing;
using Utilities;
using Zenject;

namespace Services
{
    public class IAPService : IStoreListener, IIAPService
    {
        public event Action<string> OnProcessPurchaseEvent;
        
        private IStoreController _storeController;
        private IAppleExtensions _appleExtensions;

        private Dictionary<string, IPurchaseCommand> _purchaseCommandMap;

        private IPageNavigationService _pageNavigation;
        private IPlayerProgressService _progressService;
        private ISkinsService _skinsService;

        private readonly FinanceSection _financeSection;
        private readonly IAPConfig _config;
        private readonly ICustomLogger _logger;
        
        public IAPService(IAPConfig iapConfig, FinanceSection financeSection)
        {
            _config = iapConfig;
            _financeSection = financeSection;
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IPageNavigationService pageNavigation, IPlayerProgressService progressService, ISkinsService skinsService)
        {
            _pageNavigation = pageNavigation;
            _progressService = progressService;
            _skinsService = skinsService;
        }

        public void Initialize()
        {
            BindUI();
            InitializePurchasing();
            
            _purchaseCommandMap = new Dictionary<string, IPurchaseCommand>
            {
                { _config.productsInfo.money5KID, new PurchaseMoney(_config.productsInfo.money5КQuantity, _progressService, _pageNavigation, _config.productsInfo.money5KPreview) },
                { _config.productsInfo.money10KID, new PurchaseMoney(_config.productsInfo.money10KQuantity, _progressService, _pageNavigation, _config.productsInfo.money10KPreview) },
                { _config.productsInfo.money15KID, new PurchaseMoney(_config.productsInfo.money15KQuantity, _progressService, _pageNavigation, _config.productsInfo.money15KPreview) },
                { _config.productsInfo.fastFoodPackID, new PurchaseFastFoodPack(_skinsService, _pageNavigation, _config.productsInfo.fastFoodPackPreview) },
                { _config.productsInfo.turnOffAdsID, new PurchaseOffAds(_pageNavigation, _config.productsInfo.noAdsPreview) },
            };
        }

        public Product GetProduct(string productID)
        {
            if (_storeController != null && !string.IsNullOrEmpty(productID))
            {
                return _storeController.products.WithID(productID);
            }

            _logger.PrintError("IAP","Attempted to get unknown product " + productID);
            return null;
        }

        public void Restore()
        {
            _appleExtensions.RestoreTransactions(OnRestore);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _appleExtensions = extensions.GetExtension<IAppleExtensions>();

            _logger.PrintInfo("IAP","In-App Purchasing successfully initialized");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;

            _purchaseCommandMap[product.definition.id].Execute();
            
            OnProcessPurchaseEvent?.Invoke(product.definition.id);
            
            _logger.PrintInfo("IAP",$"Purchase - {product.definition.id}");
            
            return PurchaseProcessingResult.Complete;
        }

        private void ConfirmPurchase(string productId)
        {
            _pageNavigation.ConfirmWindow.Open(() => _storeController.InitiatePurchase(productId));
        }

        private void BindUI()
        {
            _financeSection.noAdsIAP.Setup(_config.productsInfo.turnOffAdsID, ConfirmPurchase);
            _financeSection.money5kIAP.Setup(_config.productsInfo.money5KID, ConfirmPurchase);
            _financeSection.money10kIAP.Setup(_config.productsInfo.money10KID, ConfirmPurchase);
            _financeSection.money15kIAP.Setup(_config.productsInfo.money15KID, ConfirmPurchase);
            _financeSection.fastFoodPackIAP.Setup(_config.productsInfo.fastFoodPackID, ConfirmPurchase);
        }

        private void InitializePurchasing()
        {
            StandardPurchasingModule module = StandardPurchasingModule.Instance();
            if (_config.useFakeStore)
                module.useFakeStoreUIMode = FakeStoreUIMode.DeveloperUser;

            var builder = ConfigurationBuilder.Instance(module);
            builder.AddProducts(CreateProducts());
            
            UnityPurchasing.Initialize(this, builder);
        }

        private void OnRestore(bool success, string message)
        {
            _logger.PrintInfo("IAP", $"Restore: {success}; {message}");
        }

        private IEnumerable<ProductDefinition> CreateProducts()
        {
            var money5KPayout = new PayoutDefinition(PayoutType.Currency, string.Empty, _config.productsInfo.money5КQuantity);
            var money10KPayout = new PayoutDefinition(PayoutType.Currency, string.Empty, _config.productsInfo.money10KQuantity);
            var money15KPayout = new PayoutDefinition(PayoutType.Currency, string.Empty, _config.productsInfo.money15KQuantity);
            
            var products = new List<ProductDefinition>
            {
                new (_config.productsInfo.fastFoodPackID, ProductType.NonConsumable),
                new (_config.productsInfo.turnOffAdsID, ProductType.NonConsumable),
                new (_config.productsInfo.money5KID, _config.productsInfo.money5KID, ProductType.Consumable, true, money5KPayout),
                new (_config.productsInfo.money10KID, _config.productsInfo.money10KID, ProductType.Consumable, true, money10KPayout),
                new (_config.productsInfo.money15KID, _config.productsInfo.money15KID, ProductType.Consumable, true, money15KPayout)
            };

            return products;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            _logger.PrintError("IAP",$"In-App Purchasing initialize failed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            _logger.PrintError("IAP",$"In-App Purchasing initialize failed: {error}; {message}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _logger.PrintError("IAP",$"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }
    }
} 