using System.Collections.Generic;
using MatchingCards;
using SaveSystem;

namespace Pipeline
{
    public class LevelsProvider : ILevelsProvider
    {
        public Dictionary<GamesTypes, PipelineLevelData[]> PipelineLevelsDictionary { get; private set; }
        public MatchingCardsLevelData[] MatchingCardsLevels { get; private set; }

        public void SetLevels(Dictionary<GamesTypes, PipelineLevelData[]> levelsDictionary, MatchingCardsLevelData[] levels)
        {
            PipelineLevelsDictionary = levelsDictionary;
            MatchingCardsLevels = levels;
        }
    }
}