using System;

namespace Services.Ads
{
    public interface IAdsService
    {
        event Action OnRewardAdLoadedEvent;
        event Action OnRewardAdShowCompleteEvent;
        bool RewardAdLoaded { get; }
        void Initialize();
        void ShowRewardAd();
        void ShowInterstitialAd();
    }
}