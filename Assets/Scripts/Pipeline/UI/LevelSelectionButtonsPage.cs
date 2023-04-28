using UnityEngine;

namespace Pipeline
{
    public class LevelSelectionButtonsPage : MonoBehaviour
    {
        public RectTransform selectionButtonsContainer;
        private PipelineSelectionButton[] _selectionButtons;

        public void Initialize(PipelineSelectionButton[] levelSelectionButtons)
        {
            _selectionButtons = levelSelectionButtons;
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            foreach (var button in _selectionButtons)
            {
                button.Initialize(selectionButtonsContainer);
            }
        }
    }
}
