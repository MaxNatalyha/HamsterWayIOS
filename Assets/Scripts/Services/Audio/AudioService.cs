using System.Collections.Generic;
using SettingsScripts;
using UnityEngine;
using Utilities;

namespace Services
{
    public class AudioService : IAudioService
    {
        private readonly AudioConfig _audioConfig;
        private readonly SceneAudioSources _sceneAudioSources;
        private readonly Dictionary<AudioTypes, AudioClip> _audioDictionary;
        
        private readonly ICustomLogger _logger;

        public AudioService(AudioConfig audioConfig, SceneAudioSources sceneAudioSources)
        {
            _audioConfig = audioConfig;
            _sceneAudioSources = sceneAudioSources;
            _audioDictionary = new Dictionary<AudioTypes, AudioClip>(audioConfig.AudioPairs.Count);
            
            _logger = new DebugLogger();
        }

        public void Initialize()
        {
            foreach (var audioPair in _audioConfig.AudioPairs)
                _audioDictionary.Add(audioPair.type, audioPair.audioClip);
            
            _sceneAudioSources.MainThemeSource.clip = _audioConfig.MainTheme;

            if (Settings.Music)
                _sceneAudioSources.MainThemeSource.Play();

            Settings.OnMusicSettingChangeEvent += OnMusicChange;
            
            _logger.PrintInfo("Audio Service", "Initialized");
        }

        public void OnMusicChange(bool music)
        {
            if (music)
                _sceneAudioSources.MainThemeSource.Play();
            else
                _sceneAudioSources.MainThemeSource.Stop();
            
            _logger.PrintInfo("Audio Service", $"Main theme {music}");
        }

        public void PlaySound(AudioTypes type)
        {
            if(!Settings.Sound) return;

            _sceneAudioSources.SoundSource.clip = _audioDictionary[type];
            _sceneAudioSources.SoundSource.Play();
            
            _logger.PrintInfo("Audio Service", $"Play {type} sound");
        }
    }
}