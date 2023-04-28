using UnityEngine;

namespace MatchingCards
{
    public class CardMatchPair
    {
        public readonly Sprite Sprite;
        public readonly int Id;

        public CardMatchPair(Sprite sprite, int id)
        {
            Sprite = sprite;
            Id = id;
        }
    }
}
