using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/SkinColorsVariants")]
    public class SkinColorsVariants : Skin
    {
        public override ColorSkinAsset[] ColorVariants => _colorVariants;
        public override bool HasColorVariants => true;
        
        [SerializeField] private ColorSkinAsset[] _colorVariants;

        public override Color GetCurrentColorVariant()
        {
            return ColorVariants[SelectedVariantId].color;
        }

        public override List<SkinAssetSaveData> GetAssetsData()
        {
            var data = new List<SkinAssetSaveData>(_colorVariants.Length + 1);
            
            data.Add(BaseAsset.GetData());
            data.AddRange(ColorVariants.Select(asset => asset.GetData()));
            
            return data;
        }
        
        public override void SetAssetsData(Dictionary<string, bool> assetsData)
        {
            base.SetAssetsData(assetsData);
            
            foreach (var asset in ColorVariants)
                asset.SetData(assetsData);
        }

        public override void ResetSkin(bool available = false)
        {
            BaseAsset.Bought = true;
            
            foreach (var colorVariant in ColorVariants)
                colorVariant.Bought = false;

            ColorVariants[0].Bought = available;
        }
    }
}