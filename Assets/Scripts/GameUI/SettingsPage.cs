using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameUI
{
    public class SettingsPage : PageUI
    {
        [Space(10)] [Header("Settings Page")] 
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restoreButton;
        [SerializeField] private Button _languageButton;
    
        private IPageNavigationService _pageNavigationService;
        private IIAPService _iapService;

        [Inject]
        public void Construct(IPageNavigationService pageNavigationService, IIAPService iapService)
        {
            _pageNavigationService = pageNavigationService;
            _iapService = iapService;
        }
        
        private void Awake()
        {
            _backButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.Main).ClosePage(CurrentPage));
            _languageButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.LanguageSelection));
            _restoreButton.onClick.AddListener(() => _iapService.Restore());
        }
    }
}
