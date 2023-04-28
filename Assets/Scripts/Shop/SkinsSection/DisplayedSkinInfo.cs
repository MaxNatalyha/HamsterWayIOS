using SkinSystem;

namespace Shop.SkinsSection
{
    public class DisplayedSkinInfo
    {
        public int VariantId { get; set; }
        public ISkin Skin { get; set; }
        public ISkinAsset SkinAssetInfo { get; set; }
    }
}