using System;
using Utilities;

namespace Services
{
    public class LocalizationService : ILocalizationService
    {
        public event Action OnLanguageChangeEvent;

        private readonly LocalizationData _localizationData;
        private readonly ISaver _saver;
        private readonly ICustomLogger _customLogger;
        
        private const string LanguagePrefs = "Language";


        public LanguageEnum CurrentLanguage
        {
            get => _currentLanguage;
            set
            {
                _currentLanguage = value;
                OnLanguageChangeEvent?.Invoke();
                SaveLanguage();
            }

        }
        private LanguageEnum _currentLanguage;
        
        public LocalizationService(LocalizationData localizationData)
        {
            _localizationData = localizationData;
            _saver = new PrefsSaver();
            _customLogger = new DebugLogger();
        }

        public void Load()
        {
            LoadLanguage();
        }
        
        public string GetLocalizedText(string key)
        {
            return GetLocalizedText(key, _currentLanguage);
        }

        public string GetLocalizedText(string key, LanguageEnum language)
        {
            if (!_localizationData.LocalizationUnits.Contains(_localizationData.LocalizationUnits.Find(k => k.UnitKey.Equals(key))))
            {
                _customLogger.PrintError("Localization Service", $"Localized text {key} missed");
                return "Localization miss";
            }
            
            LocalizationUnit unit = _localizationData.LocalizationUnits.Find(k => k.UnitKey.Equals(key));

            var currentLanguageText = language switch
            {
                LanguageEnum.Russia => unit.Russian,
                LanguageEnum.Srpski => unit.Srpski,
                LanguageEnum.English => unit.English,
                LanguageEnum.Turkish => unit.Turkish,
                LanguageEnum.Italian => unit.Italian,
                LanguageEnum.French => unit.French,
                LanguageEnum.German => unit.German,
                LanguageEnum.Dutch => unit.Dutch,
                LanguageEnum.Spanish => unit.Spanish,
                LanguageEnum.Portuguese => unit.Portuguese,
                LanguageEnum.Japanese => unit.Japanese,
                LanguageEnum.Chinese => unit.Chinese,
                LanguageEnum.Korean => unit.Korean,
                LanguageEnum.Vietnamese => unit.Vietnamese,
                _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
            };

            return currentLanguageText;
        }

        private void LoadLanguage()
        {
            if (_saver.HasKey(LanguagePrefs))
                _currentLanguage = (LanguageEnum)_saver.GetInt(LanguagePrefs);
            else
                _currentLanguage = _localizationData.DefaultLanguage;
            
            _customLogger.PrintInfo("Localization Service", $"{CurrentLanguage} language load");
        }
        

        private void SaveLanguage()
        {
            _saver.SetInt(LanguagePrefs, (int)CurrentLanguage);
            _customLogger.PrintInfo("Localization Service", $"{CurrentLanguage} language save");
        }
    }
}
