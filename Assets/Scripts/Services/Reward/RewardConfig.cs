using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Services/RewardConfiguration")]
    public class RewardConfig : ScriptableObject
    {
        public int levelsToShowAds;
        public int levelsToStoreReview;
        [Range(0f, 1f)] public float replayMltp;
        public AnimationCurve petSatietyCurve;
        
        [Header("Reward curves")]
        public AnimationCurve matchingCardRewardCurve;
        public AnimationCurve easyRewardCurve;
        public AnimationCurve classicRewardCurve;
        public AnimationCurve hardRewardCurve;
    }
}