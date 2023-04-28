using Pet;
using Services;
using Services.Ads;
using UnityEngine.UI;
using Zenject;

namespace GameUI
{
    public class MainPage : PageUI
    {
        public PetView petView;
        public FoodBarView foodBarView;
        
        public Button playButton;
        public Button settingsButton;
        public Button shopButton;
        
        public RewardAdButton foodPacketAdsButton;
        public FoodPacketView foodPacketView;

        private IPageNavigationService _pageNavigationService;

        [Inject]
        public void Construct(IPageNavigationService pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
        }

        private void Start()
        {
            playButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.GameSelection).ClosePage(CurrentPage));
            settingsButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.Settings).ClosePage(CurrentPage));
            shopButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.Shop).ClosePage(CurrentPage));
        }
    }
}
