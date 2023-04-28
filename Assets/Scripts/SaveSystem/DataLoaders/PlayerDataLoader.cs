using UnityEngine;
using Utilities;

namespace SaveSystem
{
    public class PlayerDataLoader : IDataLoader<PlayerData>
    {
        private readonly string _savePath;
        private readonly ISaveSystem _saveSystem;
        private readonly ICustomLogger _logger;

        public PlayerDataLoader()
        {
            _savePath = Application.persistentDataPath + "/playerData.hamsterWay";
            _saveSystem = new JsonSaveSystem(_savePath);
            _logger = new DebugLogger();
        }
        
        public PlayerData Load()
        {
            var loadedData = _saveSystem.Load<PlayerData>() ?? CreateDefaultPlayerData();
            _logger.PrintInfo("Player Data Loader", "Loaded player data");

            return loadedData;
        }

        public void Save(PlayerData saveData)
        {
            _saveSystem.Save(saveData); 
            _logger.PrintInfo("Player Data Loader", $"Player data saved to {_savePath}");
        }

        private PlayerData CreateDefaultPlayerData()
        {
            var defaultPlayerData = new PlayerData(250, 100);
        
            _logger.PrintInfo("Player Data Loader", "Created default player data");        
            return defaultPlayerData;
        }
    }
}
