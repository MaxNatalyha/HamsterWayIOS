using UnityEngine;

namespace Services
{
    public class SceneAudioSources : MonoBehaviour
    {
        public AudioSource MainThemeSource => _mainThemeSource;
        public AudioSource SoundSource => _soundSource;
        
        [SerializeField] private AudioSource _mainThemeSource;
        [SerializeField] private AudioSource _soundSource;
    }
}