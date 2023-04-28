using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using SkinSystem;
using Utilities;

namespace Services
{
    public class SkinsService : ISkinsService
    {
        public Dictionary<SkinCategories, List<Skin>> SkinsDictionary => _skinsDictionary;
        public Dictionary<SkinCategories, SkinNames> SelectedSkins => _selectedSkins;
        private readonly Dictionary<SkinCategories, List<Skin>> _skinsDictionary;
        private readonly Dictionary<SkinCategories, SkinNames> _selectedSkins;

        private readonly SkinsConfig _skinsConfig;
        private readonly ICustomLogger _logger;
        private readonly ISaver _saver;

        public SkinsService(SkinsConfig skinsConfig)
        {
            _skinsConfig = skinsConfig;

            _logger = new DebugLogger();
            _saver = new PrefsSaver();
            
            _skinsDictionary = new Dictionary<SkinCategories, List<Skin>>();
            _selectedSkins = new Dictionary<SkinCategories, SkinNames>();
        }

        public void Load(Dictionary<string, bool> skinAssetsData)
        {
            LoadSelectedSkins();
            SetSkinsData(skinAssetsData);
        }

        public void Initialize()
        {
            foreach (var category in _skinsConfig.skinsCategories)
                _skinsDictionary.Add(category.category, category.skins);
            
            _logger.PrintInfo("Skin Service", "Initialized");
        }

        public ISkin GetSkin(SkinCategories category)
        {
            var selectedSkinName = _selectedSkins[category];
            return _skinsDictionary[category].Find(s => s.SkinName.Equals(selectedSkinName));
        }

        public void SelectSkin(SkinCategories category, SkinNames skinName)
        {
            _selectedSkins[category] = skinName;
        }

        public void UnlockSkin(SkinCategories category, SkinNames skinName)
        {
            _skinsDictionary[category].Find(s => s.SkinName.Equals(skinName)).BaseAsset.Bought = true;
        }

        public Dictionary<string, bool> Save()
        {
            SaveSelectedSkins();
            return GetSkinsAssetsData();
        }

        private Dictionary<string, bool> GetSkinsAssetsData()
        {
            var buffer = new List<SkinAssetSaveData>();

            foreach (var skin in _skinsDictionary.Values.SelectMany(skinsCategory => skinsCategory))
                buffer.AddRange(skin.GetAssetsData());

            return buffer.ToDictionary(assetSaveData => assetSaveData.id, assetSaveData => assetSaveData.bought);
        }

        private void SetSkinsData(Dictionary<string, bool> skinAssetsData)
        {
            if(skinAssetsData == null) return;
            
            foreach (var skinsCategory in _skinsConfig.skinsCategories)
                foreach (var skin in skinsCategory.skins)
                    skin.SetAssetsData(skinAssetsData);
        }

        private void SaveSelectedSkins()
        {
            foreach (var pair in _selectedSkins)
                _saver.SetInt(pair.Key.ToString(), (int)pair.Value);
        }

        private void LoadSelectedSkins()
        {
            _selectedSkins.Add(SkinCategories.House, LoadSelectedSkin(SkinCategories.House));
            _selectedSkins.Add(SkinCategories.Pipes, LoadSelectedSkin(SkinCategories.Pipes));
            _selectedSkins.Add(SkinCategories.Bowl, LoadSelectedSkin(SkinCategories.Bowl));
            _selectedSkins.Add(SkinCategories.Card, LoadSelectedSkin(SkinCategories.Card));
            _selectedSkins.Add(SkinCategories.Pet, LoadSelectedSkin(SkinCategories.Pet));
        }

        private SkinNames LoadSelectedSkin(SkinCategories category)
        {
            var selectedSkin = _saver.HasKey(SkinCategories.House.ToString())
                ? (SkinNames)_saver.GetInt(category.ToString())
                : SkinNames.Default;

            return selectedSkin;
        }
    }
}
