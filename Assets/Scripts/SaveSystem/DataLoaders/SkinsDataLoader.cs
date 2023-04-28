using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace SaveSystem
{
    public class SkinsDataLoader : IDataLoader<Dictionary<string, bool>>
    {
        private readonly string _savePath;
        private readonly ISaveSystem _saveSystem;
        private readonly ICustomLogger _logger;

        public SkinsDataLoader()
        {
            _savePath = Application.persistentDataPath + "/skins.data";
            _saveSystem = new JsonSaveSystem(_savePath);
            _logger = new DebugLogger();
        }
        
        public Dictionary<string, bool> Load()
        {
            var loadedData = _saveSystem.Load<Dictionary<string, bool>>();
            _logger.PrintInfo("Skins Data Loader", "Loaded skins data");

            return loadedData;
        }

        public void Save(Dictionary<string, bool> saveData)
        {
            _saveSystem.Save(saveData);
            _logger.PrintInfo("Skins Data Loader", $"Skins data saved to {_savePath}");
        }
    }
}
