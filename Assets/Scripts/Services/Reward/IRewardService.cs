using GameUI;
using SaveSystem;

namespace Services
{
    public interface IRewardService
    {
        void AddReward(int starsAmount, ILevelData levelData, ILevelPopup popup);
        void DoubleReward();
    }
}