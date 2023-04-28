using System;
using Pet;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class FoodInShopView : MonoBehaviour
    {
        [SerializeField] private Image _purchaseIcon, _purchaseCurrency, _fillbar;
        [SerializeField] private Sprite _coinSprite, _moneySprite;
        [SerializeField] private Text _purchasePriceField, _amountField;
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _amountBlock;
        [SerializeField] private Color _availableColor, _notAvailableColor;

        private IPetFood _petFood;
        private IPlayerProgressService _playerProgressService;
        private IShopController _shopController;

        public void Initialize(IPetFood petFood, IPlayerProgressService progressService, IShopController shopController)
        {
            _petFood = petFood;
            _button.onClick.AddListener(() => _shopController.BuyFood(_petFood));
            
            _playerProgressService = progressService;
            _shopController = shopController;
            
            SetupView();
        }
        
        private void SetupView()
        {
            _purchaseIcon.sprite = _petFood.Config.FoodSprite;
            _purchasePriceField.text = _petFood.Config.PurchaseInfo.Price.ToString();
            
            _purchaseCurrency.sprite = _petFood.Config.PurchaseInfo.Currency switch
            {
                CurrencyType.Coins => _coinSprite,
                CurrencyType.Money => _moneySprite,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _fillbar.fillAmount = _petFood.Config.Satiety / 100f;
        }

        private void UpdateView(int newAmount)
        {
            if(_petFood.State.IsEmpty)
            {
                _amountBlock.gameObject.SetActive(false);
                return;
            }
        
            _amountBlock.gameObject.SetActive(true);
            _amountField.text = _petFood.State.Amount.ToString();
        }
    
        private void OnEnable()
        {
            UpdateView(_petFood.State.Amount);
            CheckAvailable(_petFood.Config.PurchaseInfo);
            
            _playerProgressService.OnCurrencyChangeEvent += OnCurrencyChange;
            _petFood.State.OnAmountChangeEvent += UpdateView;
        }

        private void OnDisable()
        {
            _playerProgressService.OnCurrencyChangeEvent -= OnCurrencyChange;
            _petFood.State.OnAmountChangeEvent -= UpdateView;
        }

        private void OnCurrencyChange()
        {
            CheckAvailable(_petFood.Config.PurchaseInfo);
        }
    
        private void CheckAvailable(IPurchasable purchasable)
        {
            bool isAvailable = _playerProgressService.GetCurrencyByType(purchasable.Currency) >= purchasable.Price;

            _button.enabled = isAvailable;
            _purchaseIcon.color = isAvailable ? _availableColor : _notAvailableColor;
        }
    }
}
