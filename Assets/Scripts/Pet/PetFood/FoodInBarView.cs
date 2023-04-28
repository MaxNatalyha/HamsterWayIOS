using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pet
{
    public class FoodInBarView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _foodImage, _fillBar;
        [SerializeField] private TMP_Text _amountField;
        [SerializeField] private RectTransform _amountBlock;

        private Action<FoodInBarView> _onEmptyAction;
        private IPetFood _food;
        
        public void Initialize(Action<FoodInBarView> onEmptyAction, Action<IPetFood> feedAction) 
        {
            _onEmptyAction = onEmptyAction;
            _button.onClick.AddListener(() => feedAction?.Invoke(_food));
        }

        public void SetupView(IPetFood food)
        {
            _food = food;
            _foodImage.sprite = food.Config.FoodSprite;
            _amountField.text = food.State.Amount.ToString();
            _fillBar.fillAmount = food.Config.Satiety / 100f;

            _food.State.OnAmountChangeEvent += UpdateView;
        }

        public void CleanUp()
        {
            _amountBlock.gameObject.SetActive(true);
            _food.State.OnAmountChangeEvent -= UpdateView;
            _food = null;
        }

        private void UpdateView(int newAmount)
        {
            if(_food.State.IsLast)
            {
                _amountBlock.gameObject.SetActive(false);
                return;
            }        
        
            if(_food.State.IsEmpty)
            {
                _onEmptyAction?.Invoke(this);
                return;
            }
        
            _amountField.text = _food.State.Amount.ToString();
        }
    }
}
