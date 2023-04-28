using TMPro;
using UnityEngine;

namespace GameUI
{
    public class ValueDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueField;

        private int _speed;
        private int _currentValue;
        private int _newValue;
        private float _startFontSize;
        private bool _decrease;
        private bool _increase;
    
        private void Awake()
        {
            _startFontSize = _valueField.fontSize;
        }

        public void SetField(int current)
        {
            _currentValue = current;
            _valueField.text = _currentValue.ToString();
        }

        public void UpdateField(int newAmount)
        {
            if (newAmount > _currentValue)
            {
                IncreaseField(newAmount);
            }
            else if (newAmount < _currentValue)
            {
                DecreaseField(newAmount);
            }
        }

        private void IncreaseField(int newAmount)
        {
            _newValue = newAmount;
            _valueField.fontSize = _startFontSize + 5;
            _valueField.fontStyle = FontStyles.Bold;
        
            _speed = Mathf.Clamp(_newValue - _currentValue, 150, 2500);
        
            _increase = true; 
        }

        private void DecreaseField(int newAmount)
        {
            _newValue = newAmount;
            _valueField.fontSize = _startFontSize + 5;
            _valueField.fontStyle = FontStyles.Bold;

            _speed = Mathf.Clamp(_currentValue - _newValue, 150, 2500);
        
            _decrease = true;
        }
    
        private void Update()
        {
            if(_decrease)
                Decrease();
        
            if(_increase)
                Increase();
        }
    
        private void OnTimerLeft()
        {
            _currentValue = _newValue;
            _valueField.text = _currentValue.ToString();
            _valueField.fontSize = _startFontSize;
            _valueField.fontStyle = FontStyles.Normal;
        }
    
        private void Decrease()
        {
            _currentValue = Mathf.RoundToInt(_currentValue - Time.deltaTime * _speed);
            _valueField.text = _currentValue.ToString();
        
            if (_currentValue <= _newValue)
            {
                _decrease = false;
                OnTimerLeft();
            }
        }
    
        private void Increase()
        {
            _currentValue = Mathf.RoundToInt(_currentValue + Time.deltaTime * _speed);
            _valueField.text = _currentValue.ToString();
        
            if (_currentValue >= _newValue)
            {
                _increase = false;
                OnTimerLeft();
            }
        }
    }
}
