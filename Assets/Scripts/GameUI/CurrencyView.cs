using Services;
using UnityEngine;
using Zenject;

namespace GameUI
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private ValueDisplay _coinsField;
        [SerializeField] private ValueDisplay _moneyField;

        private IPlayerProgressService _playerProgress;

        private int _coins => _playerProgress.GetCurrencyByType(CurrencyType.Coins);
        private int _money => _playerProgress.GetCurrencyByType(CurrencyType.Money);

        [Inject]
        public void Construct(IPlayerProgressService playerProgressService)
        {
            _playerProgress = playerProgressService;
        }

        private void OnEnable()
        {
            _playerProgress.OnCurrencyChangeEvent += OnCurrencyChange;
        
            _coinsField.SetField(_coins);
            _moneyField.SetField(_money);
        }

        private void OnDisable()
        {
            _playerProgress.OnCurrencyChangeEvent -= OnCurrencyChange;
        }

        private void OnCurrencyChange()
        {
            _coinsField.UpdateField(_coins);
            _moneyField.UpdateField(_money);
        }
    }
}
