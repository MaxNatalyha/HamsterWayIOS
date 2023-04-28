using System;
using System.Collections.Generic;
using SaveSystem;
using Services;
using UnityEngine;

namespace SkinSystem
{
    public class SkinAsset : ScriptableObject, ISkinAsset
    {
        public CurrencyType Currency => _currency;
        public int Price => _price;
        public SkinAssetID AssetID => _assetID;

        public bool Bought
        {
            get => _bought;
            set => _bought = value;
        }

        public bool IAP
        {
            get => _iap;
            set => _iap = value;
        }
        public virtual Color PreviewColor { get; }

        [SerializeField] private CurrencyType _currency;
        [SerializeField] private int _price;
        [SerializeField] private bool _bought;
        [SerializeField] private bool _iap;
        [SerializeField] private SkinAssetID _assetID;

        public void SetData(Dictionary<string, bool> assetsData)
        {
            if(assetsData.ContainsKey(AssetID.value))
                Bought = assetsData[AssetID.value];
        }

        public SkinAssetSaveData GetData()
        {
            return new SkinAssetSaveData(AssetID.value, Bought);
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_assetID.value))
                _assetID.value = Guid.NewGuid().ToString();
        }
    }
}