using LevelGenerator;
using Pipeline;
using Utilities;

namespace StateMachineCore.GameStates
{
    public class HamsterRunningState : IState
    {
        private readonly HamsterPathFinder _pathFinder;
        private readonly PipelineGameLevelPage _levelPage;
        private readonly IPipelineObjectsProvider _objectsProvider;
        private readonly ICustomLogger _customLogger;

        public HamsterRunningState(PipelineGameLevelPage levelPage, IPipelineObjectsProvider objectsProvider)
        {
            _levelPage = levelPage;
            _objectsProvider = objectsProvider;
            _pathFinder = new HamsterPathFinder();
            _customLogger = new DebugLogger();
        }

        public void Enter()
        {
            _customLogger.PrintInfo("Hamster Run State", "Enter");

            _levelPage.upperPanel.TimeBar.Pause();
            
            _levelPage.levelView.SkipHamsterButton.gameObject.SetActive(true);
            _levelPage.levelView.RaycastBlock.gameObject.SetActive(true);
            
            RunHamster(_objectsProvider.HamsterHouse, _objectsProvider.Hamster);
            ToggleLevelObjectsViewParent();
        }

        public void Exit()
        {
            _objectsProvider.Hamster.CleanUp();
            _levelPage.levelView.RaycastBlock.gameObject.SetActive(false);
            _levelPage.levelView.SkipHamsterButton.gameObject.SetActive(false);
            
            ToggleLevelObjectsViewParent();
        }

        private void RunHamster(HamsterHouse house, Hamster hamster)
        {
            var hamsterPath = _pathFinder.FindHamsterPath(house.GetHouseConnectors());
            hamster.StartMoving(hamsterPath);
        }

        private void ToggleLevelObjectsViewParent()
        {
            foreach (var pipe in _objectsProvider.Pipes)
            {
                pipe.ToggleViewParent();
            }
            
            _objectsProvider.HamsterHouse.ToggleViewParent();
        }
    }
}