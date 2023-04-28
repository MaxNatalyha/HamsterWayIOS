using MatchingCards;

namespace SaveSystem
{
    public interface ILevelLoader
    {
        MatchingCardsLevelData[] LoadMatchingCardsLevels();
        PipelineLevelData[] LoadPipelineLevelsByType(GamesTypes type);
    }
}