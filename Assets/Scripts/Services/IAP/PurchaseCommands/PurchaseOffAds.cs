using SettingsScripts;
using UnityEngine;
using Utilities;

namespace Services.PurchaseCommands
{
    public class PurchaseOffAds : IPurchaseCommand
    {
        private readonly Sprite _noAdsPreview;
        private readonly IPageNavigationService _pageNavigation;
        private readonly ICustomLogger _logger;
        
        public PurchaseOffAds(IPageNavigationService pageNavigation, Sprite noAdsPreview)
        {
            _pageNavigation = pageNavigation;
            _noAdsPreview = noAdsPreview;
            _logger = new DebugLogger();
        }
        
        public void Execute()
        {
            Settings.SetSetting(SettingsEnum.InterstitialAds, false);

            _pageNavigation.NotificationWindow.Open("TurnOff Ads", _noAdsPreview);
            _logger.PrintInfo("IAP","Purchased turn off ads");
        }
    }
}