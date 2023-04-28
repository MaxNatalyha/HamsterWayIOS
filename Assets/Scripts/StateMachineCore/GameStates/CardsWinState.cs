using GameUI;
using LevelGenerator;
using SaveSystem;
using Services;

namespace StateMachineCore.GameStates
{
    public class CardsWinState : WinState
    {
        private readonly ICardsProvider _cards;

        public CardsWinState(LevelPageUpperPanel upperPanel, IRewardService rewardService, ICardsProvider cards, ILevelPopup levelPopup) : base(upperPanel, rewardService, levelPopup) 
        {
            _cards = cards;
        }

        public override void Enter(ILevelData level)
        {
            base.Enter(level);
            
            _cards.CloseAllCards();
        }
    }
}