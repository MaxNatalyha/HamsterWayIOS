using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Services
{
    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour
    {
        [SerializeField] private AudioTypes _type;

        private Button _button;
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void Initialize(IAudioService audioService, AudioTypes type)
        {
            _audioService = audioService;
            _type = type;
        }
    
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => _audioService.PlaySound(_type));
        }
    }
}
