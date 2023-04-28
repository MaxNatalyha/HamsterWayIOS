using System;
using Services;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameUI
{
    public class LevelHelpButton : MonoBehaviour
    {
        [SerializeField] private Text _priceField;
        [SerializeField] private Button _button;
        [SerializeField] private Image _currencyIcon, _purchaseIcon;
        [SerializeField] private Sprite _coinsIcon, _moneyIcon;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LevelHelpButtonConfig _config;
    
        private Action _onBuyAction;
        private IPlayerProgressService _playerProgressService;

        [Inject]
        public void Construct(IPlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;
        }
    
        public void SetAction(Action onBuy)
        {
            _onBuyAction = onBuy;
        }

        private void Start()
        {
            SetUpUI();
        }

        private void SetUpUI()
        {
            _priceField.text = _config.Price.ToString();
            _purchaseIcon.sprite = _config.Icon;

            _currencyIcon.sprite = _config.Currency switch
            {
                CurrencyType.Coins => _coinsIcon,
                CurrencyType.Money => _moneyIcon,
                _ => throw new ArgumentOutOfRangeException()
            };

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if(_playerProgressService.SpendCurrency(_config.Price, _config.Currency)) 
                _onBuyAction?.Invoke();
        }

        private void OnEnable()
        {
            _playerProgressService.OnCurrencyChangeEvent += OnCurrencyChange;
            CheckAvailable(_config);
        }

        private void OnDisable()
        {
            _playerProgressService.OnCurrencyChangeEvent -= OnCurrencyChange;
        }

        private void OnCurrencyChange()
        {
            CheckAvailable(_config);
        }

        private void CheckAvailable(IPurchasable purchasable)
        {
            bool isAvailable = _playerProgressService.GetCurrencyByType(purchasable.Currency) >= purchasable.Price;

            _button.enabled = isAvailable;
            _canvasGroup.alpha = isAvailable ? 1f : .5f;
        }
    }
}
