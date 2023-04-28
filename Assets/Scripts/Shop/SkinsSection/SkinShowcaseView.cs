using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.SkinsSection
{
    public class SkinShowcaseView : MonoBehaviour
    { 
        public AbstractSkinSectionView[] skinViews;

        public RectTransform iapPanel, pricePanel, selectedPanel, variantsContainer, variantsPool;
        public Button buyButton, selectButton, leftButton, rightButton;
        public TMP_Text priceText;
        public CanvasGroup buyButtonCanvasGroup;
        public Image currencyImage;
        public Sprite coinSprite, moneySprite;
    }
}