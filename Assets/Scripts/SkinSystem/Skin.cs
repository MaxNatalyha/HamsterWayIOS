using System.Collections.Generic;
using SaveSystem;
using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Skin")]
    public class Skin : ScriptableObject, ISkin
    {
        public SkinCategories Category => _category;
        public SkinNames SkinName => _skinName;

        public SkinAsset BaseAsset => _baseAsset;

        public virtual SkinAsset[] AssetsVariants { get; }
        public virtual ColorSkinAsset[] ColorVariants { get; }

        public bool Lock => !BaseAsset.Bought;

        public virtual bool HasColorVariants => false;
        public virtual bool HasAssetVariants => false;

        public int SelectedVariantId
        {
            get => _selectedVariantId;
            set => _selectedVariantId = value;
        }
        
        [SerializeField] private SkinCategories _category;
        [SerializeField] private SkinNames _skinName;

        [SerializeField] private SkinAsset _baseAsset;
        [SerializeField] private int _selectedVariantId;
        
        public virtual T GetCurrentAssetVariant<T>() where T : SkinAsset
        {
            return BaseAsset as T;
        }

        public virtual Color GetCurrentColorVariant()
        {
            return Color.white;
        }
        
        public virtual List<SkinAssetSaveData> GetAssetsData()
        {
            var data = new List<SkinAssetSaveData>();
            
            data.Add(BaseAsset.GetData());
            
            return data;
        }
        
        public virtual void SetAssetsData(Dictionary<string, bool> assetsData)
        {
            BaseAsset.SetData(assetsData);
        }

        public virtual void ResetSkin(bool available = false)
        {
            _baseAsset.Bought = available;
        }
    }
}

