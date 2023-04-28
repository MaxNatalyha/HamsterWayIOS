using System;

namespace SaveSystem
{
    [Serializable]
    public class RewardLevelSaveData
    {
        public byte duration;
        public byte[] starsPercent = new byte[3];
    }
}