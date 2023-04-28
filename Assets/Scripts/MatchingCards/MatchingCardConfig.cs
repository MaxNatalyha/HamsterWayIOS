using System.Collections.Generic;
using UnityEngine;

namespace MatchingCards
{
    [CreateAssetMenu(menuName = "MatchingCard/Config")]
    public class MatchingCardConfig : ScriptableObject
    {
        public float cardAspectRatio;
        public List<Sprite> cardFaceSprites;
    }
}