using GameUI;
using TMPro;
using UnityEngine;

namespace MatchingCards
{
    public class MatchingCardsBottomPanel : MonoBehaviour
    {
        public LevelHelpButton ExtraTime => _extraTime;
        public LevelHelpButton ExtraClick => _extraClick;
        public TMP_Text ClickAmount => _clickAmount;
        
        [SerializeField] private LevelHelpButton _extraTime;
        [SerializeField] private LevelHelpButton _extraClick;
        [SerializeField] private TMP_Text _clickAmount;
    }
}