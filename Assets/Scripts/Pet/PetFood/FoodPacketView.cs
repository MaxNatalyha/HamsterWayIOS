using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pet
{
    public class FoodPacketView : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private Image[] _foodImages = new Image[3];
        [SerializeField] private Button _closeButton;

        private IAudioService _audioService;
        private IParticleService _particleService;

        [Inject]
        public void Construct(IAudioService audioService, IParticleService particleService)
        {
            _audioService = audioService;
            _particleService = particleService;
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => _parent.gameObject.SetActive(false));
        }

        public void Show()
        {
            _parent.gameObject.SetActive(true);
        }

        public void SetFoodIcons(IPetFood[] foodSprites)
        {
            for (int i = 0; i < _foodImages.Length; i++)
                _foodImages[i].sprite = foodSprites[i].Config.FoodSprite;
        }

        private void OnEnable()
        {
            _closeButton.interactable = false;
        }

        private void ANIMATION_PacketOpen()
        {
            _particleService.PlayConfetti();
            _audioService.PlaySound(AudioTypes.OpenBox);
        }

        private void ANIMATION_Finish()
        {
            _closeButton.interactable = true;
        }
    }
}
