using System.Collections.Generic;
using GameUI;
using Pet;
using SaveSystem;
using Services.Ads;
using SettingsScripts;
using UnityEngine;
using Utilities;
using UnityEngine.iOS;
using Zenject;

namespace Services
{
    public class RewardService : IRewardService
    {
        private int _currentLevelToAds;
        private int _currentLevelToReview;

        private int _currentReward;
        private ILevelPopup _currentLevelPopup;
        
        private IAdsService _adsService;
        private IPetController _petController;
        private IParticleService _particleService;
        private IPlayerProgressService _playerProgressService;

        private readonly Dictionary<GamesTypes, AnimationCurve> _rewardsDictionary;
        private readonly RewardConfig _config;
        private readonly ICustomLogger _logger;
        
        private const int MAX_LEVEL_PROGRESS = 3;
        
        public RewardService(RewardConfig rewardConfig)
        {
            _config = rewardConfig;
            
            _rewardsDictionary = new Dictionary<GamesTypes, AnimationCurve>
            {
                { GamesTypes.MatchingCards, _config.matchingCardRewardCurve },
                { GamesTypes.EasyPipeline, _config.easyRewardCurve },
                { GamesTypes.ClassicPipeline, _config.classicRewardCurve },
                { GamesTypes.HardPipeline, _config.hardRewardCurve }
            };

            _logger = new DebugLogger();
        }
        
        [Inject]
        public void Construct(IParticleService particleService, IPlayerProgressService playerProgressService, IPetController petController, IAdsService adsService)
        {
            _particleService = particleService;
            _playerProgressService = playerProgressService;
            _petController = petController;
            _adsService = adsService;
        }

        public void AddReward(int starsAmount, ILevelData levelData, ILevelPopup popup)
        {
            _currentLevelPopup = popup;
            
            CheckShowInterstitialAds();
            CheckAskStoreReview();

            _currentReward = CalculateReward(starsAmount, levelData);
            
            _currentLevelPopup.Win(_currentReward, starsAmount);
            
            _particleService.PlayConfetti();
        
            _playerProgressService.AddCurrency(_currentReward, CurrencyType.Coins);
            _playerProgressService.SetGameLevelProgress(levelData, starsAmount);
        }

        private int CalculateReward(int starsAmount, ILevelData levelData)
        {
            float perStarReward = _rewardsDictionary[levelData.GameType].Evaluate(levelData.LevelID) / 3f;
            float reward = perStarReward * starsAmount;
        
            if (_playerProgressService.GetLevelProgressByType(levelData) == MAX_LEVEL_PROGRESS)
                reward *= _config.replayMltp;
        
            reward *= _config.petSatietyCurve.Evaluate(_petController.Satiety);

            return Mathf.RoundToInt(reward);
        }

        public void DoubleReward()
        {
            _currentLevelPopup.ShowDoubledReward(_currentReward * 2);
            _playerProgressService.AddCurrency(_currentReward, CurrencyType.Coins);
            _particleService.PlayConfetti();
            
            _logger.PrintInfo("Reward Service", "Reward doubled");
        }

        private void CheckAskStoreReview()
        {
            _currentLevelToReview++;
            if (_currentLevelToReview >= _config.levelsToStoreReview)
            {
                Device.RequestStoreReview();
                _currentLevelToReview = 0;
            }
        }

        private void CheckShowInterstitialAds()
        {
            _currentLevelToAds++;
            if (_currentLevelToAds >= _config.levelsToShowAds && Settings.ADS)
            {
                _adsService.ShowInterstitialAd();
                _currentLevelToAds = 0;
            }
        }
    }
}
