using UnityEngine;

namespace SkinSystem
{
    public interface ISkin
    {
        SkinCategories Category { get; }
        SkinNames SkinName { get; }
        
        int SelectedVariantId { get; set; }
        
        SkinAsset BaseAsset { get; }
        SkinAsset[] AssetsVariants { get; }
        ColorSkinAsset[] ColorVariants { get; }

        bool Lock { get; }
        bool HasColorVariants { get; }
        bool HasAssetVariants { get; }

        T GetCurrentAssetVariant<T>() where T : SkinAsset;
        Color GetCurrentColorVariant();
    }
}
