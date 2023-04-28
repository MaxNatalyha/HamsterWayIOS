using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class PipelineLevelData : ILevelData
    {
        public int LevelID => id;
        public GamesTypes GameType => (GamesTypes)pipelineType;
        public Vector2Int GridSize => new (gridSizeX, gridSizeY);

        public string levelName;
        public string authorName;
        public int id;
        public byte pipelineType;
        public byte gridSizeX;
        public byte gridSizeY;
        public RewardLevelSaveData rewardData;
        public List<PipelineObjectSaveData> levelObjects;

        public PipelineLevelData()
        {
            levelObjects = new List<PipelineObjectSaveData>();
        }
    }
}