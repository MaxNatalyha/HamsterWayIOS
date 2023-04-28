using Pipeline;
using Services;
using UnityEngine;
using Zenject;

namespace MatchingCards
{
    public class MatchingCardLevelSelectionPage : PageUI
    {
        public RectTransform LevelButtonsContainer => _levelsContainer;

        [Space(10)]
        [Header("Level Selection")]
        [SerializeField] private RectTransform _levelsContainer;
        [SerializeField] private SelectionPageUpperPanel _upperPanel;

        private MatchingCardSelectionButton[] _levelsButtons;

        private IPageNavigationService _pageNavigationService;
        private IPlayerProgressService _playerProgressService;

        [Inject]
        public void Construct(IPageNavigationService pageNavigationService, IPlayerProgressService playerProgressService)
        {
            _pageNavigationService = pageNavigationService;
            _playerProgressService = playerProgressService;
        }
        
        public void SetSelectionButtons(MatchingCardSelectionButton[] selectionButtonsPages)
        {
            _levelsButtons = selectionButtonsPages;
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            foreach (var levelButton in _levelsButtons)
                levelButton.Initialize();
        }
        
        private void Awake()
        {
            _upperPanel.BackButton.onClick.AddListener(() => _pageNavigationService.OpenPage(PagesEnum.GameSelection).ClosePage(CurrentPage));
        }

        private void OnEnable()
        {
            UpdateStarsField();
        }

        private void UpdateStarsField()
        {
            int playerStars = _playerProgressService.GetTotalGameProgressByType(GamesTypes.MatchingCards);
        
            _upperPanel.StarsAmountText.text = $"{playerStars}";
        }
    }
}
