using SaveSystem;
using UnityEngine;

namespace LevelGenerator
{
    public interface IPipelineLevelGenerator
    {
        void Initialize(RectTransform board, RectTransform upperViewParent);
        void Generate(PipelineLevelData pipelineLevelData);
        void CleanUp();
    }
}