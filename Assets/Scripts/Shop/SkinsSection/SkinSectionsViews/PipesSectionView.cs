using SkinSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.SkinsSection
{
    public class PipesSectionView : AbstractSkinSectionView
    {
        public override ShopSectionsEnum ShopSection => ShopSectionsEnum.Pipes;
        public override SkinCategories SkinCategory => SkinCategories.Pipes;
        
        [SerializeField] private Image _pipesImage;
        [SerializeField] private Image _pipesBackImage;

        private void ShowPipesAsset(PipesSkinAsset asset)
        {
            _pipesImage.sprite = asset.straightCrossPipe;
            _pipesBackImage.color = asset.backColor;
        }

        public override void DisplayAssetColor(Color color)
        {
            _pipesImage.color = color;
        }

        public override void DisplayAssetView(SkinAsset asset)
        {
            ShowPipesAsset(asset as PipesSkinAsset);
        }
    }
}