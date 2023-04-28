using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using Utilities;

namespace Services
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public event Action OnCurrencyChangeEvent;

        private PlayerData _playerData;
        
        private readonly Dictionary<GamesTypes, Dictionary<int, int>> _progressMap;
        private readonly ICustomLogger _logger;

        public PlayerProgressService()
        {
            _logger = new DebugLogger();
            _progressMap = new Dictionary<GamesTypes, Dictionary<int, int>>();
        }

        public void SetPlayerData(PlayerData playerData)
        {
            _playerData = playerData;
            _progressMap.Add(GamesTypes.EasyPipeline, playerData.PipelineEasyProgress);
            _progressMap.Add(GamesTypes.ClassicPipeline, playerData.PipelineClassicProgress);
            _progressMap.Add(GamesTypes.HardPipeline, playerData.PipelineHardProgress);
            _progressMap.Add(GamesTypes.MatchingCards, playerData.MatchingCardProgress);
        }

        public PlayerData GetPlayerData()
        {
            return _playerData;
        }

        public void AddCurrency(int amount, CurrencyType currency)
        {
            switch (currency)
            {
                case CurrencyType.Coins:
                    _playerData.coins += amount;
                    OnCurrencyChangeEvent?.Invoke();
                    break;
                case CurrencyType.Money:
                    _playerData.money += amount;
                    OnCurrencyChangeEvent?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
            }
        }

        public bool SpendCurrency(int amount, CurrencyType currency)
        {
            switch (currency)
            {
                case CurrencyType.Coins:
                    if (_playerData.coins - amount < 0) return false;
                    _playerData.coins -= amount;
                    
                    OnCurrencyChangeEvent?.Invoke();
                    
                    return true;
                case CurrencyType.Money:
                    if (_playerData.money - amount < 0) return false;
                    _playerData.money -= amount;
                    
                    OnCurrencyChangeEvent?.Invoke();
                    
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
            }
        }
        
        public int GetCurrencyByType(CurrencyType currency)
        {
            if (_playerData == null) return 0;
        
            return currency switch
            {
                CurrencyType.Coins => _playerData.coins,
                CurrencyType.Money => _playerData.money,
                _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
            };
        }

        public void SetGameLevelProgress(ILevelData levelData, int newProgress)
        {
            var currentGameProgress = _progressMap[levelData.GameType];
            
            if (newProgress > 0 && !currentGameProgress.ContainsKey(levelData.LevelID + 1))
            {
                currentGameProgress.Add(levelData.LevelID + 1, 0);
                _logger.PrintInfo("Progress Service",$"Unlock next level - {levelData.LevelID + 2} in {levelData.GameType}");
            }

            if (currentGameProgress.ContainsKey(levelData.LevelID) && !(currentGameProgress[levelData.LevelID] > newProgress))
            {
                currentGameProgress[levelData.LevelID] = newProgress;                
                _logger.PrintInfo("Progress Service",$"Set new progress - {newProgress} stars for {levelData.LevelID + 1} level of {levelData.GameType}");
            }
            else if (!currentGameProgress.ContainsKey(levelData.LevelID))
            {
                currentGameProgress.Add(levelData.LevelID, newProgress);
                _logger.PrintInfo("Progress Service", $"Add new progress - {newProgress} stars for {levelData.LevelID + 1} level of {levelData.GameType}");
            }
        }
        
        public int GetLevelProgressByType(ILevelData levelData)
        {
            return _progressMap[levelData.GameType].ContainsKey(levelData.LevelID)
                ? _progressMap[levelData.GameType][levelData.LevelID]
                : -1;
        }

        public int GetTotalGameProgressByType(GamesTypes gameType)
        {
            return _progressMap[gameType].Sum(progress => progress.Value);
        }
    }
}
