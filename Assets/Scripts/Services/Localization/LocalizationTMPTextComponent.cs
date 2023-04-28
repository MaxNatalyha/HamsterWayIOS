using TMPro;
using UnityEngine;
using Zenject;

namespace Services
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationTMPTextComponent : MonoBehaviour
    {
        [SerializeField] private string _unitKey;
        private TMP_Text _textField;
        private ILocalizationService _localizationService;

        [Inject]
        public void Construct(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        
        private void Awake()
        {
            _textField = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            UpdateText();
            _localizationService.OnLanguageChangeEvent += UpdateText;
        }
        
        private void OnDisable()
        {
            _localizationService.OnLanguageChangeEvent -= UpdateText;
        }

        private void UpdateText()
        {
            _textField.text = _localizationService.GetLocalizedText(_unitKey);
        }
    }
}