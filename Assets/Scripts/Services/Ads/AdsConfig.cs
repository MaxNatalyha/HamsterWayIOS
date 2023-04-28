using UnityEngine;

namespace Services.Ads
{
    [CreateAssetMenu(menuName = "Services/AdsConfiguration")]
    public class AdsConfig : ScriptableObject
    {
        [Header("Unity Ads")]
        public string iosGameID = "4825216";
        public string androidGameID;

        public string iosRewardId = "Rewarded_iOS";
        public string androidRewardId;

        public string iosInterstitialId = "Interstitial_iOS";
        public string androidInterstitialId;
        
        [Space(10)]
        [Header("Ad Mob Android")]
        public string admobGameID;
 
        public string admobInterstitialId;
        public string admobRewardId;
        
        public string admobInterstitialIdTest;
        public string admobRewardIdTest;

        [Space(10)]
        public bool testMode;
        public bool enableAds;
    }
}