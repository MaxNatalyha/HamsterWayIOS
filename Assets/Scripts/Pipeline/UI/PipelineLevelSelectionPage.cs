using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pipeline
{
    public class PipelineLevelSelectionPage : PageUI
    {
        public RectTransform LevelButtonsContainer => _levelsContainer;
        
        [Space(10)]
        [Header("Level Selection")]
        [SerializeField] private GamesTypes _currentGameType;
        [SerializeField] private Text _buttonsPagesField;
        [SerializeField] private Button _leftScrollButton;
        [SerializeField] private Button _rigthScrollButton;
        [SerializeField] private RectTransform _levelsContainer;
        [SerializeField] private SelectionPageUpperPanel _upperPanel;

        private LevelSelectionButtonsPage[] _levelsPages;
        private int _currentPageIndex;

        private IPageNavigationService _pageNavigationService;
        private IPlayerProgressService _playerProgressService;

        [Inject]
        public void Construct(IPageNavigationService pageNavigationService, IPlayerProgressService playerProgressService)
        {
            _pageNavigationService = pageNavigationService;
            _playerProgressService = playerProgressService;
        }

        public void SetSelectionButtonsPages(LevelSelectionButtonsPage[] selectionButtonsPages)
        {
            _levelsPages = selectionButtonsPages;
            _levelsPages[0].gameObject.SetActive(true);
            _buttonsPagesField.text = $"{_currentPageIndex+1}/{_levelsPages.Length}";
        }

        private void Awake()
        {
            _upperPanel.BackButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.GameSelection).ClosePage(CurrentPage));
            _leftScrollButton.onClick.AddListener(() => FlipLevelsPage(-1));
            _rigthScrollButton.onClick.AddListener(() => FlipLevelsPage(1));
        }

        private void OnEnable()
        {
            UpdateStarsField();
        }

        private void FlipLevelsPage(int shift)
        {
            int newIndex = _currentPageIndex + shift;

            if (newIndex > _levelsPages.Length - 1)
                newIndex = 0;

            if (newIndex < 0)
                newIndex = _levelsPages.Length-1;
        
            if(newIndex == _currentPageIndex) return;

            _levelsPages[newIndex].gameObject.SetActive(true);
            _levelsPages[_currentPageIndex].gameObject.SetActive(false);

            _currentPageIndex = newIndex;
            _buttonsPagesField.text = $"{_currentPageIndex+1}/{_levelsPages.Length}";
        }

        private void UpdateStarsField()
        {
            int playerStars = _playerProgressService.GetTotalGameProgressByType(_currentGameType);
        
            _upperPanel.StarsAmountText.text = $"{playerStars}";
        }
    }
}
