using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Services/AudioConfiguration")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip MainTheme => _mainTheme;
        public List<AudioClipPair> AudioPairs => _audioPairs;
        
        [SerializeField] private AudioClip _mainTheme;
        [SerializeField] private List<AudioClipPair> _audioPairs;
    }
}