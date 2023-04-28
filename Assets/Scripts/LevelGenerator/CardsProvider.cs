using System.Collections.Generic;
using MatchingCards;

namespace LevelGenerator
{
    public class CardsProvider : ICardsProvider
    {
        public int CardsPairs => Cards.Count / 2;
        public List<Card> Cards { get; set; }

        public CardsProvider()
        {
            Cards = new List<Card>();
        }

        public void CloseAllCards()
        {
            foreach (var card in Cards)
                card.CloseCard();
        }
    }
}