using System;
using Services;
using StateMachineCore;

namespace Shop.SkinsSection.SkinViewStates
{
    public class PurchaseState : IState
    {
        private readonly SkinShowcaseView _skinShowcaseView;
        private readonly DisplayedSkinInfo _displayedSkinInfo;
        private readonly IPlayerProgressService _playerProgress;
        
        public PurchaseState(SkinShowcaseView skinShowcaseView , IPlayerProgressService playerProgress, DisplayedSkinInfo displayedSkinInfo)
        {
            _skinShowcaseView = skinShowcaseView;
            _playerProgress = playerProgress;
            _displayedSkinInfo = displayedSkinInfo;
        }

        public void Enter()
        {
            _skinShowcaseView.pricePanel.gameObject.SetActive(true);
            _skinShowcaseView.buyButton.gameObject.SetActive(true);
            
            ShowPriceInfo(_displayedSkinInfo.SkinAssetInfo);
            CheckAvailable(_displayedSkinInfo.SkinAssetInfo);
            
            if(_displayedSkinInfo.Skin.Lock && _displayedSkinInfo.VariantId != 0)
                _skinShowcaseView.buyButton.gameObject.SetActive(false);
                
            _playerProgress.OnCurrencyChangeEvent += OnCurrencyChange;
        }

        public void Exit()
        {
            _skinShowcaseView.pricePanel.gameObject.SetActive(false);
            _skinShowcaseView.buyButton.gameObject.SetActive(false);
            
            _playerProgress.OnCurrencyChangeEvent -= OnCurrencyChange;
        }
        
        private void ShowPriceInfo(IPurchasable info)
        {
            _skinShowcaseView.priceText.text = info.Price.ToString();
            _skinShowcaseView.currencyImage.sprite = info.Currency switch
            {
                CurrencyType.Coins => _skinShowcaseView.coinSprite,
                CurrencyType.Money => _skinShowcaseView.moneySprite,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void OnCurrencyChange()
        {
            CheckAvailable(_displayedSkinInfo.SkinAssetInfo);
        }
        
        private void CheckAvailable(IPurchasable purchaseInfo)
        {
            bool isAvailable = _playerProgress.GetCurrencyByType(purchaseInfo.Currency) >= purchaseInfo.Price;
            
            _skinShowcaseView.buyButtonCanvasGroup.alpha = isAvailable ? 1f : .33f;
            _skinShowcaseView.buyButton.interactable = isAvailable;
        }
    }
}