using System.Collections.Generic;
using SkinSystem;

namespace Services
{
    public interface ISkinsService
    {
        Dictionary<SkinCategories, SkinNames> SelectedSkins { get; }
        public Dictionary<SkinCategories, List<Skin>> SkinsDictionary { get; }

        void Initialize();

        ISkin GetSkin(SkinCategories category);

        void UnlockSkin(SkinCategories category, SkinNames skinName);
        void SelectSkin(SkinCategories category, SkinNames skinName);

        Dictionary<string, bool> Save();
        void Load(Dictionary<string, bool> skinAssetsData);
    }
}