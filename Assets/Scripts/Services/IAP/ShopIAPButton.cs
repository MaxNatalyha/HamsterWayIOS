using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    [RequireComponent(typeof(Button))]
    public class ShopIAPButton : MonoBehaviour
    {
        [SerializeField] private Text _priceText, _quantityText;
        [SerializeField] private Image _buttonBorder;
        [SerializeField] private Button _button;
        [SerializeField] private Color _ownedButtonColor;

        private Color _defaultButtonColor;
        private string _productId;

        private ILocalizationService _localizationService;
        private IIAPService _iapService;

        [Inject]
        public void Construct(IIAPService iapService, ILocalizationService localizationService)
        {
            _iapService = iapService;
            _localizationService = localizationService;
        }

        public void Setup(string productId, Action<string> onClickAction)
        {
            _productId = productId;
            
            _button.interactable = false;
            _button.onClick.AddListener(() => onClickAction?.Invoke(_productId));
            
            if(_buttonBorder != null)
                _defaultButtonColor = _buttonBorder.color;
        }

        private void OnEnable()
        {
            UpdateButton();
            _iapService.OnProcessPurchaseEvent += OnProcessPurchase;
        }

        private void OnDisable()
        {
            _iapService.OnProcessPurchaseEvent -= OnProcessPurchase;
        }

        private void OnProcessPurchase(string id)
        {
            if(_productId == id)
                UpdateButton();
        }

        private void UpdateButton()
        {
            var product = _iapService.GetProduct(_productId);

            if (product == null) return;
            
            _button.interactable = true;
                
            if (_priceText != null)
            {
                _priceText.text = product.metadata.localizedPriceString;
            }

            if (_quantityText != null)
            {
                _quantityText.text = product.definition.payout.quantity.ToString(CultureInfo.CurrentCulture);
            }

            if (product.definition.type == ProductType.NonConsumable)
            {
                _button.interactable = !product.hasReceipt;
                _buttonBorder.color = product.hasReceipt ? _ownedButtonColor : _defaultButtonColor;
                    
                if (product.hasReceipt)
                    _priceText.text = _localizationService.GetLocalizedText(LocalizationKeys.BOUGHT_KEY);
            }
        }
    }
}
