using System;
using Utilities;

namespace SettingsScripts
{
    public static class Settings
    {
        public static event Action<bool> OnMusicSettingChangeEvent;
        
        public static bool Music { get; private set; }
        public static bool Sound { get; private set; }
        public static bool ADS { get; private set; }
        
        private static ICustomLogger _customLogger;
        private static ISaver _saver;

        public static void Initialize()
        {
            _customLogger = new DebugLogger();
            _saver = new PrefsSaver();
            
            Load();
        }

        public static void SetSetting(SettingsEnum setting, bool value)
        {
            switch (setting)
            {
                case SettingsEnum.Sound:
                    Sound = value;
                    Save(SettingsKeys.SOUND_KEY, value);
                    break;
                case SettingsEnum.Music:
                    Music = value;
                    OnMusicSettingChangeEvent?.Invoke(value);
                    Save(SettingsKeys.MUSIC_KEY, value);
                    break;
                case SettingsEnum.InterstitialAds:
                    ADS = value;
                    Save(SettingsKeys.ADS_KEY, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(setting), setting, null);
            }
            
            _customLogger.PrintInfo("Settings", $"{setting} set to {value}");
        }

        public static bool GetSetting(SettingsEnum setting)
        {
            return setting switch
            {
                SettingsEnum.Music => Music,
                SettingsEnum.Sound => Sound,
                SettingsEnum.InterstitialAds => ADS,
                _ => throw new ArgumentOutOfRangeException(nameof(setting), setting, null)
            };
        }
        
        private static void Load()
        {
            Sound = !_saver.HasKey(SettingsKeys.SOUND_KEY) || _saver.GetInt(SettingsKeys.SOUND_KEY) == 1;
            Music = !_saver.HasKey(SettingsKeys.MUSIC_KEY) || _saver.GetInt(SettingsKeys.MUSIC_KEY) == 1;
            ADS = !_saver.HasKey(SettingsKeys.ADS_KEY) || _saver.GetInt(SettingsKeys.ADS_KEY) == 1;

            _customLogger.PrintInfo("Settings", $"Music - {Music}; Sound - {Sound}; ADS - {ADS}");
        }

        private static void Save(string key, bool value)
        {
            _saver.SetInt(key, value ? 1 : 0);
            _customLogger.PrintInfo("Setting", $"Save {key} {value}");
        }
    }
}
