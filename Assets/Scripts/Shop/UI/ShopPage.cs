using Services;
using Shop.SkinsSection;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shop
{
    public class ShopPage : PageUI
    {
        [Space(10)] [Header("Shop Page")]
        public Button backButton;
        public ShopCategoryButton[] categoriesButtons;
        public SectionsButtonsPanel[] sectionPanels;
        public PetFoodSection petFoodSection;
        public SkinShowcaseView skinShowcaseView;
        public FinanceSection financeSection;
        
        private IShopController _shopController;

        [Inject]
        public void Construct(IShopController shopController)
        {
            _shopController = shopController;
        }
        
        private void OnEnable() => _shopController.OpenCategory(ShopCategoriesEnum.Pets);
    }
}
