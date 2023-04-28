using System;
using System.Linq;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class LevelTimeBar : MonoBehaviour
    {
        public event Action OnTimeLeftEvent;
        public event Action<float> OnTimerTickEvent;
        public int ActiveStarsAmount => _stars.ToList().FindAll(s => s.isActive).Count;

        [SerializeField] private Image _timeBar;
        [SerializeField] private LevelTimeStar[] _stars = new LevelTimeStar[3];

        private bool _isLaunch;
        private float _timer;
        private float _levelDuration;
        private RectTransform _rect;
        private float _barLength;

        public void Initialize()
        {
            _rect = GetComponent<RectTransform>();
            _barLength = _rect.rect.width;
            for (int i = 0; i < _stars.Length; i++)
            {
                OnTimerTickEvent += _stars[i].OnTimeLeft;
            }
        }

        public void AddExtraTime(float extraTime)
        {
            _timer = Mathf.Clamp(_timer - extraTime, 0, _levelDuration);
        }

        public void SetRewardData(RewardLevelSaveData rewardData)
        {
            _levelDuration = rewardData.duration == 0 ? 150 : rewardData.duration;
            _timer = 0;

            for (int i = 0; i < _stars.Length; i++)
            {
                int percent = rewardData.starsPercent[i] == 0 ? (i * 25 + 25) : rewardData.starsPercent[i];
            
                var xPos = _barLength - ((percent * _barLength) / 100f);
                _stars[i].SetStar(xPos, _levelDuration * (percent / 100f));
            }
        
            _isLaunch = true;
        }

        public void Pause()
        {
            _isLaunch = false;
        }

        public void Resume()
        {
            _isLaunch = true;
        }

        private void Update()
        {
            if(!_isLaunch) return;

            _timer += Time.deltaTime;
            OnTimerTickEvent?.Invoke(_timer);
        
            float t = Mathf.Lerp(1, 0, _timer / _levelDuration);
            _timeBar.fillAmount = t;

            if (_timer >= _levelDuration)
            {
                OnTimeLeftEvent?.Invoke();
            
                _timer = 0;
                _isLaunch = false;
            }
        }
    }
}
