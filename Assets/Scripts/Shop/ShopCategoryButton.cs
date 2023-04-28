using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    [RequireComponent(typeof(Button))]
    public class ShopCategoryButton : MonoBehaviour
    {
        [SerializeField] private ShopCategoriesEnum _category;
        [SerializeField] private Button _button;
        
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _deselectedColor;

        private IShopController _shopController;

        public void Initialize(IShopController shopController)
        {
            _shopController = shopController;
            
            _button.onClick.AddListener(() => _shopController.OpenCategory(_category));
            _shopController.OnCategoryChangeEvent += OnCategoryChange;
        }
        
        private void OnCategoryChange(ShopCategoriesEnum category)
        {
            _button.image.color = _category == category ? _selectedColor : _deselectedColor;
        }
    }
}