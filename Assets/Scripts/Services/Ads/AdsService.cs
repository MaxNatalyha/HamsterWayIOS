using System;
using UnityEngine.Advertisements;
using Utilities;

namespace Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action OnRewardAdLoadedEvent;
        public event Action OnRewardAdShowCompleteEvent;
        public bool RewardAdLoaded { get; private set; }
        
        private string _rewardId;
        private string _interstitialId;

        private readonly AdsConfig _config;
        private readonly ICustomLogger _logger;
        
        public AdsService(AdsConfig config)
        {
            _config = config;
            _logger = new DebugLogger();
        }

        public void Initialize()
        {
            _rewardId = _config.iosRewardId;
            _interstitialId = _config.iosInterstitialId;
            
            if(_config.enableAds)
                Advertisement.Initialize(_config.iosGameID, _config.testMode, this);
        }

        public void ShowRewardAd()
        {
            if (!Advertisement.isInitialized)
            {
                _logger.PrintError("Advertisement", "Not initialize");
                return;
            }
            
            Advertisement.Show(_rewardId, this);
        }

        public void ShowInterstitialAd()
        {
            if (!Advertisement.isInitialized)
            {
                _logger.PrintError("Advertisement", "Not initialize");
                return;
            }
                
            Advertisement.Show(_interstitialId, this);
        }

        public void OnInitializationComplete()
        {
            Advertisement.Load(_interstitialId, this);
            Advertisement.Load(_rewardId, this);
            
            _logger.PrintInfo("Advertisement", "Initialization complete");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            _logger.PrintError("Advertisement", "Initialization failed");
        }
        
        

        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (placementId == _rewardId)
            {
                RewardAdLoaded = true;
                OnRewardAdLoadedEvent?.Invoke();
            }   
            
            _logger.PrintInfo("Advertisement", $"{placementId} ad loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            if (placementId == _rewardId)
                RewardAdLoaded = false;
            
            _logger.PrintError("Advertisement", $"{placementId} load failure, {message}, {error}");
        }
        
        

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            _logger.PrintError("Advertisement", $"{placementId} ad show failure: {message}, {error}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            _logger.PrintInfo("Advertisement", $"{placementId} ad show start");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            _logger.PrintInfo("Advertisement", $"{placementId} ad show click");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == _rewardId)
            {
                OnRewardAdShowCompleteEvent?.Invoke();
                RewardAdLoaded = false;
            }

            Advertisement.Load(placementId, this);

            _logger.PrintInfo("Advertisement", $"{placementId} ad show complete");
        }
    }
}