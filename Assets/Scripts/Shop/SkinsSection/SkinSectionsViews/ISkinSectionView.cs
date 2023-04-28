using SkinSystem;
using UnityEngine;

namespace Shop.SkinsSection
{
    public interface ISkinSectionView
    {
        ShopSectionsEnum ShopSection { get; }
        SkinCategories SkinCategory { get; }
        void DisplayAssetColor(Color color);
        void DisplayAssetView(SkinAsset asset);
        void OnSectionChange(ShopSectionsEnum section);
        void CleanUp();
    }
}