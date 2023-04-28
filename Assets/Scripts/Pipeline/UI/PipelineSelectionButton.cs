using GameUI;
using SaveSystem;
using Services;
using UnityEngine;

namespace Pipeline
{
    public class PipelineSelectionButton : AbstractLevelSelectionButton<LevelSelectionButtonView>
    {
        public PipelineSelectionButton(LevelSelectionButtonView view, ILevelData pipelineLevel, IPlayerProgressService playerProgressService, IGame pipelineGame) 
            : base(view, playerProgressService, pipelineGame, pipelineLevel) { }

        public void Initialize(RectTransform parent)
        {
            SetUpView();
            View.transform.SetParent(parent);
        }
    }
}
