using SkinSystem;
using UnityEngine;

namespace Shop.SkinsSection
{
    public abstract class AbstractSkinSectionView : MonoBehaviour, ISkinSectionView
    {
        public abstract ShopSectionsEnum ShopSection { get; }
        public abstract SkinCategories SkinCategory { get; }

        public abstract void DisplayAssetColor(Color color);
        public abstract void DisplayAssetView(SkinAsset asset);

        public void OnSectionChange(ShopSectionsEnum section)
        {
            gameObject.SetActive(ShopSection == section);
        }

        public void CleanUp()
        {
            DisplayAssetColor(Color.white);
        }
    }
}