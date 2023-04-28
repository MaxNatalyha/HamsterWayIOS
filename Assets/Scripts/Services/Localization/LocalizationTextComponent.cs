using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    public class LocalizationTextComponent : MonoBehaviour
    {
        public string UnitKey;
        
        private Text _textField;
        private ILocalizationService _localizationService;

        [Inject]
        public void Construct(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        
        private void Awake()
        {
            _textField = GetComponent<Text>();
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
            _textField.text = _localizationService.GetLocalizedText(UnitKey);
        }
    }
}
