using System.Collections.Generic;
using MatchingCards;
using SaveSystem;

namespace Pipeline
{
    public interface ILevelsProvider
    {
        Dictionary<GamesTypes, PipelineLevelData[]> PipelineLevelsDictionary { get; }
        MatchingCardsLevelData[] MatchingCardsLevels { get; }
        void SetLevels(Dictionary<GamesTypes, PipelineLevelData[]> levelsDictionary, MatchingCardsLevelData[] levels);
    }
}