using SkinSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Pet
{
    public class PetPaws : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _leftPawImage;
        [SerializeField] private Image _rightPawImage;
        [SerializeField] private Image _eatingFoodImage;

        public void ApplySkin(PetSkinAsset skinAsset)
        {
            _leftPawImage.sprite = skinAsset.paw;
            _rightPawImage.sprite = skinAsset.paw;
        }

        public void StartEating(Sprite foodIcon)
        {
            _eatingFoodImage.sprite = foodIcon;
            gameObject.SetActive(true);
        }

        private void ANIMATION_OnFinish()
        {
            gameObject.SetActive(false);
        }
    }
}
