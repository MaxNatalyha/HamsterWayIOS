using SkinSystem;
using UnityEngine;
using Utilities;

namespace Services.PurchaseCommands
{
    public class PurchaseFastFoodPack : IPurchaseCommand
    {
        private readonly Sprite _fastFoodPreview;
        private readonly IPageNavigationService _pageNavigation;
        private readonly ISkinsService _skinsService;
        private readonly ICustomLogger _logger;
        
        public PurchaseFastFoodPack(ISkinsService skinsService, IPageNavigationService pageNavigation, Sprite fastFoodPreview)
        {
            _pageNavigation = pageNavigation;
            _fastFoodPreview = fastFoodPreview;
            _skinsService = skinsService;
            _logger = new DebugLogger();
        }
        
        public void Execute()
        {
            _skinsService.UnlockSkin(SkinCategories.House, SkinNames.FastFood);
            _skinsService.UnlockSkin(SkinCategories.Pipes, SkinNames.FastFood);
            _skinsService.UnlockSkin(SkinCategories.Bowl, SkinNames.FastFood);
            _skinsService.UnlockSkin(SkinCategories.Card, SkinNames.FastFood);

            _pageNavigation.NotificationWindow.Open("Fast Food Pack", _fastFoodPreview, SelectFastFoodPack);
            _logger.PrintInfo("IAP","Purchased Fast Food Pack");
        }

        private void SelectFastFoodPack()
        {
            _skinsService.SelectSkin(SkinCategories.House, SkinNames.FastFood);
            _skinsService.SelectSkin(SkinCategories.Pipes, SkinNames.FastFood);
            _skinsService.SelectSkin(SkinCategories.Bowl, SkinNames.FastFood);
            _skinsService.SelectSkin(SkinCategories.Card, SkinNames.FastFood);
        }
    }
}