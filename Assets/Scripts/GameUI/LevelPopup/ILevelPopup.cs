using Services.Ads;

namespace GameUI
{
    public interface ILevelPopup
    {
        RewardAdButton AdsButton { get; }
        void Initialize(LevelPopupView view);
        void Pause();
        void Lose(string loseReasonKey);
        void Win(int reward, int progress);
        void ShowDoubledReward(int reward);
        void Close();
    }
}
