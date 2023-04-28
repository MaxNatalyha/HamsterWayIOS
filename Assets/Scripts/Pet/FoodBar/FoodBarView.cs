using System;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pet
{
    public class FoodBarView : MonoBehaviour
    {
        public RectTransform FoodHolder => _foodHolder;
        public RectTransform FoodPoolContainer => _foodPoolContainer;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private RectTransform _foodHolder;
        [SerializeField] private RectTransform _foodPoolContainer;
        [SerializeField] private Button _barButton;
        [SerializeField] private Image _miniBar;
        [SerializeField] private Image _bigBar;
        [SerializeField] private Text _satietyLevelField;

        private ILocalizationService _localizationService;
        private IPetController _petController;
        private Action<bool> _onToggleBar;
        
        private const string OPEN_ANIM_KEY = "Open";
        private bool _isOpen;

        [Inject]
        public void Construct(ILocalizationService localizationService, IPetController petController)
        {
            _localizationService = localizationService;
            _petController = petController;
        }

        public void Initialize(Action<bool> onToggleBar)
        {
            _animator.keepAnimatorStateOnDisable = true;
            _barButton.onClick.AddListener(ToggleFoodBar);
            _onToggleBar = onToggleBar;
            
            UpdateSatietyInfo(_petController.Satiety);
        }
        
        private void ToggleFoodBar()
        {
            _isOpen = !_isOpen;
            _animator.SetBool(OPEN_ANIM_KEY, _isOpen);
            
            _onToggleBar?.Invoke(_isOpen);
        }

        private void OnEnable()
        {
            _petController.OnSatietyLevelChangeEvent += UpdateSatietyInfo;
        }        
        
        private void OnDisable()
        {
            _petController.OnSatietyLevelChangeEvent -= UpdateSatietyInfo;
        }

        private void ANIMATION_OnOpenBar()
        {
            _foodHolder.gameObject.SetActive(true);
        }        
        
        private void ANIMATION_OnCloseBar()
        {
            _foodHolder.gameObject.SetActive(false);
        }
        
        private void UpdateSatietyInfo(float satiety)
        {
            var fillAmount = satiety / 100;
        
            _miniBar.fillAmount = fillAmount;
            _bigBar.fillAmount = fillAmount;

            _satietyLevelField.text = _localizationService.GetLocalizedText(LocalizationKeys.SATIETY_KEY) + $" {satiety}/100";
        }
    }
}
