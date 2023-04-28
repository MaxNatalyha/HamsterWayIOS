using System;
using System.Collections.Generic;

namespace SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public int coins;
        public int money;

        public Dictionary<int, int> PipelineEasyProgress;
        public Dictionary<int, int> PipelineClassicProgress;
        public Dictionary<int, int> PipelineHardProgress;
        public Dictionary<int, int> MatchingCardProgress;

        public PlayerData(int coins, int money)
        {
            this.coins = coins;
            this.money = money;

            PipelineEasyProgress = new Dictionary<int, int> { { 0, 0 } };
            PipelineClassicProgress = new Dictionary<int, int> { { 0, 0 } };
            PipelineHardProgress = new Dictionary<int, int> { { 0, 0 } };
            MatchingCardProgress = new Dictionary<int, int> { { 0, 0 } };
        }
    }
}