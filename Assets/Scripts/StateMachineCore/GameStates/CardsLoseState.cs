using GameUI;
using LevelGenerator;

namespace StateMachineCore.GameStates
{
    public class CardsLoseState : LoseState
    {
        private readonly ICardsProvider _cardsProvider;
        
        public CardsLoseState(LevelPageUpperPanel upperPanel, ICardsProvider cardsProvider, ILevelPopup levelPopup) : base(upperPanel, levelPopup)
        {
            _cardsProvider = cardsProvider;
        }

        public override void Enter(string loseReason)
        {
            base.Enter(loseReason);
            _cardsProvider.CloseAllCards();
        }
    }
}