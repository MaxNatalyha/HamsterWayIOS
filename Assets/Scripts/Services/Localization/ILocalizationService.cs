using System;

namespace Services
{
    public interface ILocalizationService
    {
        event Action OnLanguageChangeEvent;
        
        LanguageEnum CurrentLanguage { get; set; }

        void Load();
        string GetLocalizedText(string key);
        string GetLocalizedText(string key, LanguageEnum language);
    }
}