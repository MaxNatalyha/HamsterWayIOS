using SkinSystem;
using UnityEngine;

namespace Shop.SkinsSection
{
    public class PetSectionView : AbstractSkinSectionView
    {
        public override ShopSectionsEnum ShopSection => ShopSectionsEnum.Pets;
        public override SkinCategories SkinCategory => SkinCategories.Pet;
        
        [SerializeField] private Animator _petAnimator;

        private void ShowPetAsset(PetSkinAsset asset)
        {
            _petAnimator.runtimeAnimatorController = asset.animator;
        }

        public override void DisplayAssetColor(Color color)
        {
            
        }

        public override void DisplayAssetView(SkinAsset asset)
        {
            ShowPetAsset(asset as PetSkinAsset);
        }
    }
}