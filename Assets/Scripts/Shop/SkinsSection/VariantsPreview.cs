using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SkinSystem;
using UnityEngine;

namespace Shop.SkinsSection
{
    public class VariantsPreview
    {
        private readonly RectTransform _variantsContainer;
        private readonly ShopSkinVariantPool _skinVariantPool;
        private readonly SkinShowcase _skinShowcase;
        
        private readonly List<SkinVariantView> _displayedVariantViews;
        private readonly List<ISkinAsset> _displayedVariantInfo;

        public VariantsPreview(SkinShowcase skinShowcase, RectTransform poolContainer, RectTransform variantsContainer)
        {
            _skinVariantPool = new ShopSkinVariantPool(poolContainer);
            _displayedVariantViews = new List<SkinVariantView>();
            _displayedVariantInfo = new List<ISkinAsset>();
            _skinShowcase = skinShowcase;
            _variantsContainer = variantsContainer;
        }

        public async UniTask Load()
        {
            await _skinVariantPool.Load();
        }

        public void Initialize()
        {
            _skinVariantPool.Initialize();
        }

        public void ShowSkinVariants(ISkin skin)
        {
            if (skin.HasAssetVariants)
            {
                _displayedVariantInfo.Add(skin.BaseAsset);
                
                for (int i = 0; i < skin.AssetsVariants.Length; i++)
                    _displayedVariantInfo.Add(skin.AssetsVariants[i]);
            }
            else
            {
                for (int i = 0; i < skin.ColorVariants.Length; i++)
                    _displayedVariantInfo.Add(skin.ColorVariants[i]);
            }

            DisplayVariantsPreview(skin.Lock);
        }

        public void RefreshPreview(ISkin skin)
        {
            for (int i = 0; i < _displayedVariantInfo.Count; i++)
                _displayedVariantViews[i].Refresh(_displayedVariantInfo[i], i, skin.Lock);
        }

        public void CleanPreviewPanel()
        {
            if(_displayedVariantViews.Count == 0) return;

            foreach (var variant in _displayedVariantViews)
            {
                _skinShowcase.OnVariantPreviewChangeEvent -= variant.OnSelectedVariantChange;
                variant.CleanUp();
                _skinVariantPool.ReturnToPool(variant);
            }
            
            _displayedVariantInfo.Clear();
            _displayedVariantViews.Clear();
        }

        private void DisplayVariantsPreview(bool isLock)
        {
            for (int i = 0; i < _displayedVariantInfo.Count; i++)
            {
                var variantView = _skinVariantPool.GetVariantView();
                variantView.Setup(_displayedVariantInfo[i], i, _variantsContainer, _skinShowcase.DisplaySkinVariant, isLock);
                
                _skinShowcase.OnVariantPreviewChangeEvent += variantView.OnSelectedVariantChange;
                
                _displayedVariantViews.Add(variantView);
            }
        }
    }
}