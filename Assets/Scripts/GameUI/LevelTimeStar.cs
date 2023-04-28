using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GameUI
{
    public class LevelTimeStar : MonoBehaviour
    {
        public bool isActive = true;
    
        [SerializeField] private Image _iconField;
        [SerializeField] private Sprite _activeStar, _noactiveStar;
        [SerializeField] private RectTransform _rectTransform;

        private float _turnOffTime;
        private ICustomLogger _customLogger;

        private void Awake()
        {
            _customLogger = new DebugLogger();
        }

        public void SetStar(float xPos, float turnoffTime)
        {
            _iconField.sprite = _activeStar;
            isActive = true;
            _rectTransform.anchoredPosition = new Vector2(xPos, _rectTransform.anchoredPosition.y);
            _turnOffTime = turnoffTime;
        }

        public void OnTimeLeft(float timerTime)
        {
            if (timerTime >= _turnOffTime && isActive)
            {
                _iconField.sprite = _noactiveStar;
                isActive = false;
                _customLogger.PrintInfo($"{name}", $"Turn off at {timerTime}");
            } else if (timerTime < _turnOffTime && !isActive)
            {
                _iconField.sprite = _activeStar;
                isActive = true;
            }
        }
    }
}
