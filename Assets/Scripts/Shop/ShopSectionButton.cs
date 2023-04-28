using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    [RequireComponent(typeof(Button))]
    public class ShopSectionButton : MonoBehaviour
    {
        [SerializeField] private ShopSectionsEnum _section;
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _button;
        
        private IShopController _shopController;

        private const string ANIMATOR_BOOL = "Select";

        public void Initialize(IShopController shopController)
        {
            _animator.keepAnimatorStateOnDisable = true;
            _shopController = shopController;
            _button.onClick.AddListener(() => _shopController.OpenSection(_section));
        }

        private void OnEnable() => _shopController.OnSectionChangeEvent += OnSectionChange;

        private void OnDisable() => _shopController.OnSectionChangeEvent -= OnSectionChange;

        private void OnSectionChange(ShopSectionsEnum section)
        {
            _animator.SetBool(ANIMATOR_BOOL, _section == section);
        }
    }
}
