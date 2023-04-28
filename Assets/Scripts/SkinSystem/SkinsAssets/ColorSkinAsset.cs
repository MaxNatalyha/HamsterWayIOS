using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Assets/Color")]
    public class ColorSkinAsset : SkinAsset
    {
        public override Color PreviewColor => color;

        public Color color;
    }
}
