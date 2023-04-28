using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Assets/Card")]
    public class CardSkinAsset : SkinAsset
    {
        public override Color PreviewColor => assetColorPreview;

        [Space(10)]
        public Color assetColorPreview;
        public Sprite cardBack;
        public Sprite cardShopPreview;
    }
}
