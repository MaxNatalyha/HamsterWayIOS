using System;
using Utilities;
using Zenject;

namespace Services
{
    public class PageNavigationService : IPageNavigationService
    {
        public IConfirmWindow ConfirmWindow => _pagesProvider.ConfirmWindow;
        public INotificationWindow NotificationWindow => _pagesProvider.NotificationWindow;
        
        private IPagesProvider _pagesProvider;
        private readonly ICustomLogger _logger;

        public PageNavigationService()
        {
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IPagesProvider pagesProvider)
        {
            _pagesProvider = pagesProvider;
        }
        
        public IPageNavigationService OpenPage(PagesEnum pageToOpen)
        {
            var page = FindPage(pageToOpen);
            page.Open();

            //_logger.PrintInfo("Page Navigation Service", $"{pageToOpen} page open");
            
            return this;
        }

        public IPageNavigationService OpenPage(GamesTypes selectionPageType)
        {
            var pageType = selectionPageType switch
            {
                GamesTypes.MatchingCards => PagesEnum.MatchingCardsSelection,
                GamesTypes.EasyPipeline => PagesEnum.PipelineEasySelection,
                GamesTypes.ClassicPipeline => PagesEnum.PipelineClassicSelection,
                GamesTypes.HardPipeline => PagesEnum.PipelineHardSelection,
                _ => throw new ArgumentOutOfRangeException(nameof(selectionPageType), selectionPageType, null)
            };

            return OpenPage(pageType);
        }

        public IPageNavigationService ClosePage(PagesEnum pageToClose)
        {
            var page = FindPage(pageToClose);
            page.Close();

            //_logger.PrintInfo("Page Navigation Service", $"{pageToClose} page close");
            
            return this;        
        }

        public IPageNavigationService ClosePage(GamesTypes selectionPageType)
        {
            var pageType = selectionPageType switch
            {
                GamesTypes.MatchingCards => PagesEnum.MatchingCardsSelection,
                GamesTypes.EasyPipeline => PagesEnum.PipelineEasySelection,
                GamesTypes.ClassicPipeline => PagesEnum.PipelineClassicSelection,
                GamesTypes.HardPipeline => PagesEnum.PipelineHardSelection,
                _ => throw new ArgumentOutOfRangeException(nameof(selectionPageType), selectionPageType, null)
            };

            return ClosePage(pageType);
        }

        private IPageUI FindPage(PagesEnum pageToFind)
        {
            if (!_pagesProvider.PagesDictionary.ContainsKey(pageToFind))
            {
                _logger.PrintError("Page Navigation Service", $"{pageToFind} page does not exist");
                throw new NullReferenceException();
            }

            IPageUI page = _pagesProvider.PagesDictionary[pageToFind];
            return page;
        }
    }
}
