using Pipeline;
using SaveSystem;
using Services;

namespace GameUI
{
    public abstract class AbstractLevelSelectionButton<TView> where TView : LevelSelectionButtonView
    {
        private int _currentProgress;
        
        protected readonly TView View;
        protected readonly ILevelData LevelData;

        private readonly IPlayerProgressService _progressService;
        private readonly IGame _game;

        protected AbstractLevelSelectionButton(TView view, IPlayerProgressService progressService, IGame game, ILevelData levelData)
        {
            _progressService = progressService;
            _game = game;
            
            LevelData = levelData;
            View = view;
        }

        protected virtual void SetUpView()
        {
            View.levelIdText.text = (LevelData.LevelID + 1).ToString();
            View.selectButton.onClick.AddListener(() => _game.StartGame(LevelData));
            View.OnEnableAction = UpdateState;

            _currentProgress = _progressService.GetLevelProgressByType(LevelData);
            SetState(_currentProgress);
        }

        private void UpdateState()
        {
            int newProgress = _progressService.GetLevelProgressByType(LevelData);
            if (newProgress == _currentProgress) return;
                    
            SetState(newProgress);
            _currentProgress = newProgress;
        }

        private void SetState(int newProgress)
        {
            switch (newProgress)
            {
                case -1:
                    View.lockPanel.gameObject.SetActive(true);
                    View.selectButton.interactable = false;
                    break;
                case 0:
                    View.lockPanel.gameObject.SetActive(false);
                    View.selectButton.interactable = true;
                    break;
                case > 0:
                    View.lockPanel.gameObject.SetActive(false);
                    View.selectButton.interactable = true;
                    SetStars(newProgress);
                    break;
            }
            
            _currentProgress = newProgress;
        }

        private void SetStars(int levelProgress)
        {
            for (int i = 0; i < levelProgress; i++)
                View.starsImages[i].sprite = View.activeStar;
        }
    }
}