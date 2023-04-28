using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class LevelPageUpperPanel : MonoBehaviour
    {
        public TMP_Text LevelText => _levelText;
        public Button PauseButton => _pauseButton;
        public LevelTimeBar TimeBar => _timeBar;
        
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private LevelTimeBar _timeBar;
    }
}