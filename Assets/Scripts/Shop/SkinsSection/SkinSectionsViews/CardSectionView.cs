using SkinSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.SkinsSection
{
    public class CardSectionView : AbstractSkinSectionView
    {
        public override ShopSectionsEnum ShopSection => ShopSectionsEnum.Cards;
        public override SkinCategories SkinCategory => SkinCategories.Card;
        
        [SerializeField] private Image _cardImage;
        
        private void ShowCardAsset(CardSkinAsset asset)
        {
            _cardImage.sprite = asset.cardShopPreview;
        }

        public override void DisplayAssetColor(Color color)
        {
            _cardImage.color = color;
        }

        public override void DisplayAssetView(SkinAsset asset)
        {
            ShowCardAsset(asset as CardSkinAsset);
        }
    }
}