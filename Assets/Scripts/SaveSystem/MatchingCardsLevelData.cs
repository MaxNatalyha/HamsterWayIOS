using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(menuName = "MatchingCard/LevelData")]
    public class MatchingCardsLevelData : ScriptableObject, ILevelData
    {
        public GamesTypes GameType => GamesTypes.MatchingCards;
        public int LevelID => levelID;
        public Vector2Int GridSize => gridSize;

        public int levelID;
        public Vector2Int gridSize;
        public int clicksAmount;
        public RewardLevelSaveData rewardData;
    }
}
