using System;
using SaveSystem;

namespace Services
{
    public interface IPlayerProgressService
    {
        event Action OnCurrencyChangeEvent;

        void SetPlayerData(PlayerData playerData);
        PlayerData GetPlayerData();
        
        void AddCurrency(int amount, CurrencyType currency);
        bool SpendCurrency(int amount, CurrencyType currency);
        int GetCurrencyByType(CurrencyType currency);
        
        void SetGameLevelProgress(ILevelData levelData, int newProgress);
        int GetLevelProgressByType(ILevelData levelData);
        int GetTotalGameProgressByType(GamesTypes gameType);
    }
}