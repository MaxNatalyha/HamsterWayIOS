using Services;
using Unity.Advertisement.IosSupport;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class GameState : IGlobalState
    {
        private IPageNavigationService _pageNavigationService;
        
        private readonly ICustomLogger _logger;
        private readonly ISaver _saver;

        private const string FIRST_LAUNCH_KEY = "FirstLaunch";

        public GameState()
        {
            _logger = new DebugLogger();
            _saver = new PrefsSaver();
        }

        [Inject]
        public void Construct(IPageNavigationService pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
        }
        
        public void Enter()
        {
            _logger.PrintInfo("Global Game State", "Enter");

            _pageNavigationService.OpenPage(PagesEnum.Main).ClosePage(PagesEnum.Loading);
            
            if (!_saver.HasKey(FIRST_LAUNCH_KEY))
                FirstLaunch();
        }
        
        private void FirstLaunch()
        {
            _pageNavigationService.OpenPage(PagesEnum.LanguageSelection);
            _saver.SetInt(FIRST_LAUNCH_KEY, 1);
            CheckAtt();
        }
        
        private void CheckAtt()
        {
            var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
        }
    }
}