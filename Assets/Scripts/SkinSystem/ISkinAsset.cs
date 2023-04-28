using Shop;
using UnityEngine;

namespace SkinSystem
{
    public interface ISkinAsset : IPurchasable
    {
        SkinAssetID AssetID { get; }
        bool Bought { get; set; }
        bool IAP { get; }
        Color PreviewColor { get; }
    }
}