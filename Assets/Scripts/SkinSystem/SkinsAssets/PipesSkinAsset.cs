using UnityEngine;

namespace SkinSystem
{
    [CreateAssetMenu(menuName = "Skins/Assets/Pipes")]
    public class PipesSkinAsset : SkinAsset
    {
        public override Color PreviewColor => assetColorPreview;

        [Space(10)]
        public Color assetColorPreview;
        public Sprite straightPipe;
        public Sprite cornerPipe;
        public Sprite straightCrossPipe;
        public Sprite cornerCrossPipe;
        public Sprite housePipe;
        public Color backColor;
    }
}
