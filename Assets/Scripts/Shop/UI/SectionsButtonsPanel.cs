using UnityEngine;

namespace Shop
{
    public class SectionsButtonsPanel : MonoBehaviour
    {
        [SerializeField] private ShopSectionButton[] _sectionButtons;
        [SerializeField] private ShopCategoriesEnum _category;
        [SerializeField] private ShopSectionsEnum _firstOpenSection;

        private IShopController _shopController;
        private bool _isVisible;

        public void Initialize(IShopController shopController)
        {
            _shopController = shopController;

            foreach (var sectionButton in _sectionButtons)
                sectionButton.Initialize(shopController);
            
            _shopController.OnCategoryChangeEvent += OnCategoryChange;
        }

        private void OnCategoryChange(ShopCategoriesEnum category)
        {
            _isVisible = _category == category;
            gameObject.SetActive(_isVisible);
            
            if(_isVisible)
                _shopController.OpenSection(_firstOpenSection);
        }
    }
}