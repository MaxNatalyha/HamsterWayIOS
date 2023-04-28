using UnityEngine;
using Utilities;

namespace Services.PurchaseCommands
{
    public class PurchaseMoney : IPurchaseCommand
    {
        private readonly Sprite _moneyPreview;
        private readonly IPageNavigationService _pageNavigation;
        private readonly IPlayerProgressService _progressService;
        private readonly ICustomLogger _logger;
        private readonly int _quantity;
        
        public PurchaseMoney(int quantity, IPlayerProgressService progressService, IPageNavigationService pageNavigation, Sprite moneyPreview)
        {
            _quantity = quantity;
            _progressService = progressService;
            _pageNavigation = pageNavigation;
            _moneyPreview = moneyPreview;
            _logger = new DebugLogger();
        }
        
        public void Execute()
        {
            _progressService.AddCurrency(_quantity, CurrencyType.Money);

            _pageNavigation.NotificationWindow.Open($"+{_quantity}", _moneyPreview);
            _logger.PrintInfo("IAP",$"Purchased {_quantity} wrappers");
        }
    }
}