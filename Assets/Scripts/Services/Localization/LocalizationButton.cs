using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    [RequireComponent(typeof(Button))]
    public class LocalizationButton : MonoBehaviour
    {
        [SerializeField] private LanguageEnum _language;
        [SerializeField] private Image _selectedImage;
        [SerializeField] private Text _text;

        private const string LocalizationKey = "Language";
        private Button _button;
        
        private IPageNavigationService _pageNavigationService;
        private ILocalizationService _localizationService;
        
        [Inject]
        public void Construct(IPageNavigationService pageNavigationService, ILocalizationService localizationService)
        {
            _pageNavigationService = pageNavigationService;
            _localizationService = localizationService;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        
            _button.onClick.AddListener(OnClick);

            _text.text = _localizationService.GetLocalizedText(LocalizationKey, _language);
        }

        private void OnEnable()
        {
            _selectedImage.enabled = _localizationService.CurrentLanguage == _language;
        }

        private void OnClick()
        {
            _localizationService.CurrentLanguage = _language;
            _pageNavigationService.ClosePage(PagesEnum.LanguageSelection);
        }
    }
}
