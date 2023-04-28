using System;

namespace SaveSystem
{
    [Serializable]
    public class SkinAssetSaveData
    {
        public string id;
        public bool bought;

        public SkinAssetSaveData(string id, bool bought)
        {
            this.id = id;
            this.bought = bought;
        }
    }
}