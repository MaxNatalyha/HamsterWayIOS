using UnityEngine;

namespace SaveSystem
{
    public interface ILevelData
    {
        int LevelID { get; }
        GamesTypes GameType { get; }
        Vector2Int GridSize { get; }
    }
}