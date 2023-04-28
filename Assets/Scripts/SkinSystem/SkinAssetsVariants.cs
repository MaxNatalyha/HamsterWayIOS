using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/SkinAssetsVariants")]
    public class SkinAssetsVariants : Skin
    {
        public override SkinAsset[] AssetsVariants => _assetVariants;
        public override bool HasAssetVariants => true;
        
        [SerializeField] private SkinAsset[] _assetVariants;

        public override T GetCurrentAssetVariant<T>()
        {
            return SelectedVariantId == 0 ? BaseAsset as T : AssetsVariants[SelectedVariantId-1] as T;
        }

        public override List<SkinAssetSaveData> GetAssetsData()
        {
            var data = new List<SkinAssetSaveData>(_assetVariants.Length + 1);
            
            data.Add(BaseAsset.GetData());
            data.AddRange(AssetsVariants.Select(asset => asset.GetData()));
            
            return data;
        }

        public override void SetAssetsData(Dictionary<string, bool> assetsData)
        {
            base.SetAssetsData(assetsData);
            
            foreach (var asset in AssetsVariants)
                asset.SetData(assetsData);
        }
        
        public override void ResetSkin(bool available = false)
        {
            base.ResetSkin(available);
            
            foreach (var assetVariant in AssetsVariants)
                assetVariant.Bought = false;
        }
    }
}