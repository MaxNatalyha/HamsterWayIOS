using Cysharp.Threading.Tasks;
using MatchingCards;
using SaveSystem;
using Services;
using UnityEngine;
using Utilities;
using Zenject;

namespace GameUI
{
    public class MatchingCardsLevelsButtonsFactory
    {
        private GameObject _buttonViewPrefab;

        private IMatchingCardsGame _cardsGame;
        private IPlayerProgressService _playerProgress;
        private IAudioService _audioService;
        
        private readonly IResourceLoader _resourceLoader;

        private const string BUTTON_VIEW_KEY = "MatchingCardLevelButtonView";

        public MatchingCardsLevelsButtonsFactory()
        {
            _resourceLoader = new AddressableLoader();
        }

        [Inject]
        public void Construct(IMatchingCardsGame cardsGame, IPlayerProgressService playerProgress, IAudioService audioService)
        {
            _cardsGame = cardsGame;
            _playerProgress = playerProgress;
            _audioService = audioService;
        }
        
        public async UniTask Load()
        {
            _buttonViewPrefab = await _resourceLoader.Load<GameObject>(BUTTON_VIEW_KEY);
        }
        
        public MatchingCardSelectionButton[] Create(MatchingCardsLevelData[] levels, RectTransform buttonsContainer)
        {
            MatchingCardSelectionButton[] selectionButtons = new MatchingCardSelectionButton[levels.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                var view = Object.Instantiate(_buttonViewPrefab, buttonsContainer).GetComponent<MatchingCardSelectionButtonView>();
                view.gameObject.AddComponent<AudioButton>().Initialize(_audioService, AudioTypes.ButtonPress);
                var button = new MatchingCardSelectionButton(view, levels[i], _playerProgress, _cardsGame);
                selectionButtons[i] = button;
            }
            
            return selectionButtons;
        }
    }
}