using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameUI
{
    public class GameSelectionPage : PageUI
    {    
        [Space(10)] [Header("Game Selection Page")]
        [SerializeField] private Button _matchingCardLevelsButton;
        [SerializeField] private Button _easyPipelineLevelsButton;
        [SerializeField] private Button _classicPipelineLevelsButton;
        [SerializeField] private Button _hardPipelineLevelsButton;
        [SerializeField] private Button _backButton;

        private IPageNavigationService _pageNavigationService;

        [Inject]
        public void Construct(IPageNavigationService pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
        }

        private void Awake()
        {
            _matchingCardLevelsButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.MatchingCardsSelection).ClosePage(CurrentPage));
            _easyPipelineLevelsButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.PipelineEasySelection).ClosePage(CurrentPage));
            _classicPipelineLevelsButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.PipelineClassicSelection).ClosePage(CurrentPage));
            _hardPipelineLevelsButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.PipelineHardSelection).ClosePage(CurrentPage));
            _backButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.Main).ClosePage(CurrentPage));
        }
    }
}
