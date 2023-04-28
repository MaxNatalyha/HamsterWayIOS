using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Shop.SkinsSection
{
    public class ShopSkinVariantPool
    {
        private readonly Stack<SkinVariantView> _pool;
        private readonly RectTransform _poolContainer;
        private readonly ShopSkinVariantFactory _factory;

        private const int START_POOL_SIZE = 7;

        public ShopSkinVariantPool(RectTransform poolContainer)
        {
            _poolContainer = poolContainer;
            _pool = new Stack<SkinVariantView>(START_POOL_SIZE);
            _factory = new ShopSkinVariantFactory();
        }

        public async UniTask Load()
        {
            await _factory.Load();
        }

        public void Initialize()
        {
            CreatePool();
        }

        public SkinVariantView GetVariantView()
        {
            if(_pool.Count == 0)
                _pool.Push(_factory.Create(_poolContainer));

            return _pool.Pop();
        }

        public void ReturnToPool(SkinVariantView variantView)
        {
            variantView.transform.SetParent(_poolContainer);
            variantView.gameObject.SetActive(false);
            
            _pool.Push(variantView);
        }

        private void CreatePool()
        {
            for (int i = 0; i < START_POOL_SIZE; i++)
            {
                _pool.Push(_factory.Create(_poolContainer));
            }
        }
    }
}