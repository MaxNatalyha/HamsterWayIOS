using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Assets/House")]
    public class HouseSkinAsset : SkinAsset
    {
        public override Color PreviewColor => assetColorPreview;

        [Space(10)]
        public Color assetColorPreview;
        public Sprite house;
        public Sprite houseHole;
    }
}
