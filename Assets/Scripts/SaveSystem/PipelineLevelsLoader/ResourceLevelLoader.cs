using System;
using MatchingCards;
using Newtonsoft.Json;
using UnityEngine;
using Utilities;

namespace SaveSystem
{
    public class ResourceLevelLoader : ILevelLoader
    {
        private const string EASY_PIPELINE_PATH = "PipelineLevels/PipelineEasy";
        private const string CLASSIC_PIPELINE_PATH = "PipelineLevels/PipelineClassic";
        private const string HARD_PIPELINE_PATH = "PipelineLevels/PipelineHard";
        private const string MATCHING_CARD_PATH = "MatchingCardLevels";

        private readonly ICustomLogger _customLogger;

        public ResourceLevelLoader()
        {
            _customLogger = new DebugLogger();
        }
        
        public MatchingCardsLevelData[] LoadMatchingCardsLevels()
        {
            var levels = Resources.LoadAll<MatchingCardsLevelData>(CreatePath(GamesTypes.MatchingCards));
            
            if (levels == null)
            {
                _customLogger.PrintError("Resource Levels Loader", $"Failed to load levels for {GamesTypes.MatchingCards}");
                throw new NullReferenceException();
            }
            
            _customLogger.PrintInfo("Resource Levels Loader", $"Load {levels.Length} level for {GamesTypes.MatchingCards}");
            return levels;
        }
        
        public PipelineLevelData[] LoadPipelineLevelsByType(GamesTypes type)
        {
            var jsons = Resources.LoadAll<TextAsset>(CreatePath(type));
            
            if (jsons == null)
            {
                _customLogger.PrintError("Resource Levels Loader", $"Failed to load levels for {type}");
                throw new NullReferenceException();
            }

            var levels = new PipelineLevelData[jsons.Length];

            for (int i = 0; i < jsons.Length; i++)
            {
                levels[i] = JsonConvert.DeserializeObject<PipelineLevelData>(jsons[i].text);
            }
            
            _customLogger.PrintInfo("Resource Levels Loader", $"Load {levels.Length} level for {type}");
            return levels;
        }

        private string CreatePath(GamesTypes gameType)
        {
            string path = gameType switch
            {
                GamesTypes.EasyPipeline => EASY_PIPELINE_PATH,
                GamesTypes.ClassicPipeline => CLASSIC_PIPELINE_PATH,
                GamesTypes.HardPipeline => HARD_PIPELINE_PATH,
                GamesTypes.MatchingCards => MATCHING_CARD_PATH,
                _ => throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null)
            };

            return path;
        }        
    }
}