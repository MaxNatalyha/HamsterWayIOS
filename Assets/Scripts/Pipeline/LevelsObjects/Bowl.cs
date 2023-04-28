using SkinSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Pipeline
{
    public class Bowl : ConnectableObject
    {
        [Header("Bowl")]
        [SerializeField] private RectTransform _hamsterParent;
        [SerializeField] private Image _foodFront, _foodBack;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Hamster>())
            {
                col.transform.SetParent(_hamsterParent);
            }
        }
        
        public void ApplySkin(ISkin bowlSkin)
        {
            var bowlAsset = bowlSkin.GetCurrentAssetVariant<BowlSkinAsset>();
        
            _viewImage.sprite = bowlAsset.bowl;
            _foodFront.sprite = bowlAsset.foodFront;
            _foodBack.sprite = bowlAsset.foodBack;

            _viewImage.color = bowlSkin.HasColorVariants ? bowlSkin.GetCurrentColorVariant() : Color.white;
        }
    }
}
