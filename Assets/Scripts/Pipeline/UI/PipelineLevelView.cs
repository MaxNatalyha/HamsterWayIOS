using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    public class PipelineLevelView : MonoBehaviour
    {
        public RectTransform LevelObjectsContainer => _levelObjectsContainer;
        public RectTransform LevelUpperLayerContainer => _levelUpperLayerContainer;
        public RectTransform RaycastBlock => _raycastBlock;
        public Button SkipHamsterButton => _skipHasmterButton;
        
        [SerializeField] private RectTransform _levelObjectsContainer;
        [SerializeField] private RectTransform _levelUpperLayerContainer;
        [SerializeField] private RectTransform _raycastBlock;
        [SerializeField] private Button _skipHasmterButton;
    }
}