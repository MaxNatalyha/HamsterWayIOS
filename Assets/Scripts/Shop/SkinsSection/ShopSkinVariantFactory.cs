using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Shop.SkinsSection
{
    public class ShopSkinVariantFactory
    {
        private GameObject _skinVariantPrefab;

        private readonly IResourceLoader _resourceLoader;
        private const string VARIANT_VIEW_KEY = "ShopSkinVariantView";

        public ShopSkinVariantFactory()
        {
            _resourceLoader = new AddressableLoader();
        }

        public async UniTask Load()
        {
            _skinVariantPrefab = await _resourceLoader.Load<GameObject>(VARIANT_VIEW_KEY);
        }

        public SkinVariantView Create(RectTransform container)
        {
            var createdObject = Object.Instantiate(_skinVariantPrefab, container);
            createdObject.gameObject.SetActive(false);
            
            return createdObject.GetComponent<SkinVariantView>();
        }
    }
}