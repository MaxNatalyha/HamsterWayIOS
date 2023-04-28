using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Assets/Bowl")]
    public class BowlSkinAsset : SkinAsset
    {
        public override Color PreviewColor => assetColorPreview;

        [Space(10)]
        public Color assetColorPreview;
        public Sprite bowl;
        public Sprite foodFront;
        public Sprite foodBack;
    }
}
