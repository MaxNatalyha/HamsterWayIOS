using System.Linq;
using Cysharp.Threading.Tasks;
using Pipeline;
using SaveSystem;
using Services;
using UnityEngine;
using Utilities;
using Zenject;

namespace GameUI
{
    public class PipelineLevelsButtonsFactory
    {
        private GameObject _buttonsPagePrefab;
        private GameObject _buttonViewPrefab;

        private IPipelineGame _pipelineGame;
        private IAudioService _audioService;
        private IPlayerProgressService _playerProgress;
        
        private readonly IResourceLoader _resourceLoader;
        private readonly ICustomLogger _logger;
        
        private const string BUTTONS_PAGE_KEY = "PipelineLevelButtonsPage";
        private const string BUTTON_VIEW_KEY = "PipelineLevelButtonView";

        public PipelineLevelsButtonsFactory()
        {
            _resourceLoader = new AddressableLoader();
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IPipelineGame pipelineGame, IPlayerProgressService playerProgress, IAudioService audioService)
        {
            _pipelineGame = pipelineGame;
            _playerProgress = playerProgress;
            _audioService = audioService;
        }

        public async UniTask Load()
        {
            _buttonsPagePrefab = await _resourceLoader.Load<GameObject>(BUTTONS_PAGE_KEY);
            _buttonViewPrefab = await _resourceLoader.Load<GameObject>(BUTTON_VIEW_KEY);
        }

        public LevelSelectionButtonsPage[] Create(PipelineLevelData[] levels, int levelsButtonPerPage, Transform pagesContainer)
        {
            var buttons = CreateLevelSelectionButtons(levels, pagesContainer);
            var pages = CreateLevelSelectionsPages(buttons, levelsButtonPerPage, pagesContainer);
            return pages;
        }

        private PipelineSelectionButton[] CreateLevelSelectionButtons(PipelineLevelData[] levels, Transform pagesContainer)
        {
            PipelineSelectionButton[] selectionButtons = new PipelineSelectionButton[levels.Length];
                
            for (int i = 0; i < levels.Length; i++)
            {
                var view = Object.Instantiate(_buttonViewPrefab, pagesContainer).GetComponent<LevelSelectionButtonView>();
                view.gameObject.AddComponent<AudioButton>().Initialize(_audioService, AudioTypes.ButtonPress);
                var button = new PipelineSelectionButton(view, levels[i], _playerProgress, _pipelineGame);

                selectionButtons[i] = button;
            }

            return selectionButtons;
        }

        private LevelSelectionButtonsPage[] CreateLevelSelectionsPages(PipelineSelectionButton[] levelsButton, int levelsButtonPerPage, Transform pagesContainer)
        {
            int pagesTotal = Mathf.CeilToInt((float)levelsButton.Length / (float)levelsButtonPerPage);
            LevelSelectionButtonsPage[] levelPages = new LevelSelectionButtonsPage[pagesTotal];
                            
            for (int i = 0; i < pagesTotal; i++)
            {
                PipelineSelectionButton[] levelsRange = CalculateRange(levelsButton, levelsButtonPerPage, i, pagesTotal);
                
                LevelSelectionButtonsPage page = CreateLevelSelectionPage(levelsRange, pagesContainer);
                page.gameObject.SetActive(false);
                levelPages[i] = page;
            }
            
            _logger.PrintInfo("Pipeline Levels Buttons Factory", $"Total levels pages - {pagesTotal}, total levels buttons - {levelsButton.Length}");
            return levelPages;
        }

        private PipelineSelectionButton[] CalculateRange(PipelineSelectionButton[] levels, int levelsPerPage, int pageIndex, int pagesTotal)
        {
            PipelineSelectionButton[] levelsRange;

            if (levels.Length <= levelsPerPage)
            {
                levelsRange = levels;
            }
            else
            {
                int blockLevelsRange = levelsPerPage;
                
                if (pageIndex == pagesTotal - 1)
                {
                    blockLevelsRange = levels.Length % levelsPerPage;
                }
                    
                levelsRange = levels.ToList().GetRange(pageIndex * levelsPerPage, blockLevelsRange).ToArray();
            }

            return levelsRange;
        }
        
        private LevelSelectionButtonsPage CreateLevelSelectionPage(PipelineSelectionButton[] selectionButtons, Transform pagesContainer)
        {
            var selectionLevelPage = Object.Instantiate(_buttonsPagePrefab, pagesContainer).GetComponent<LevelSelectionButtonsPage>();
            selectionLevelPage.Initialize(selectionButtons);

            return selectionLevelPage;
        }
    }
}