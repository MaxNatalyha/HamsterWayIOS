using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services.Ads
{
    [RequireComponent(typeof(Button))]
    public class RewardAdButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private Action _onRewardAdComplete;
        private IAdsService _adsService;

        [Inject]
        public void Construct(IAdsService adsService)
        {
            _adsService = adsService;
        }

        private void Awake()
        {
            _button.onClick.AddListener(_adsService.ShowRewardAd);
        }

        public void SetRewardAdCompleteAction(Action onRewardAdComplete)
        {
            _onRewardAdComplete = onRewardAdComplete;
        }

        private void OnEnable()
        {
            if (_adsService.RewardAdLoaded)
                LoadedAdState();
            else
                NotLoadedAdState();
        }

        private void OnDisable()
        {
            _adsService.OnRewardAdLoadedEvent -= LoadedAdState;
            _adsService.OnRewardAdShowCompleteEvent -= OnRewardAdShowComplete;
        }

        private void LoadedAdState()
        {
            _adsService.OnRewardAdShowCompleteEvent += OnRewardAdShowComplete;
            _button.interactable = true;
        }

        private void NotLoadedAdState()
        {               
            _adsService.OnRewardAdLoadedEvent += LoadedAdState;
            _button.interactable = false;
        }

        private void OnRewardAdShowComplete()
        {
            _onRewardAdComplete?.Invoke();
            _button.interactable = false;
        }
    }
}