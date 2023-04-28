using System.Collections.Generic;
using MatchingCards;

namespace LevelGenerator
{
    public interface ICardsProvider
    {
        int CardsPairs { get; }
        List<Card> Cards { get; set; }
        void CloseAllCards();
    }
}