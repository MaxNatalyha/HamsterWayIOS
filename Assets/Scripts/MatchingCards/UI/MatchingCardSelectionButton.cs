using GameUI;
using SaveSystem;
using Services;

namespace MatchingCards
{
    public class MatchingCardSelectionButton : AbstractLevelSelectionButton<MatchingCardSelectionButtonView>
    {
        public MatchingCardSelectionButton(MatchingCardSelectionButtonView view, ILevelData level, IPlayerProgressService playerProgressService, IGame cardsGame) 
            : base(view, playerProgressService, cardsGame, level) { }

        public void Initialize()
        {
            SetUpView();
        }

        protected override void SetUpView()
        {
            base.SetUpView();
            View.cardsAmountText.text = LevelData.GridSize.x * LevelData.GridSize.y + " cards";
        }
    }
}