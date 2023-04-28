using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Assets/Pet")]
    public class PetSkinAsset : SkinAsset
    {
        public override Color PreviewColor => assetColorPreview;

        [Space(10)]
        public Color assetColorPreview;
        public RuntimeAnimatorController animator;
        public Sprite paw;
    }
}
