using GameUI;
using UnityEngine;

namespace Pipeline
{
    public class PipelineBottomPanel : MonoBehaviour
    {
        public LevelHelpButton ExtraTime => _extraTime;
        public LevelHelpButton SkipLevel => _skipLevel;
        
        [SerializeField] private LevelHelpButton _extraTime;
        [SerializeField] private LevelHelpButton _skipLevel;
    }
}