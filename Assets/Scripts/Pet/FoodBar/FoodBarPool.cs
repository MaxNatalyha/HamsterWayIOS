using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Pet
{
    public class FoodBarPool
    {
        private Stack<FoodInBarView> _foodViewPool;

        private RectTransform _poolContainer;
        private readonly FoodBarFactory _factory;

        private const int START_POOL_SIZE = 10;

        public FoodBarPool()
        {
            _factory = new FoodBarFactory();
        }

        public async UniTask Load()
        {
            await _factory.Load();   
        }
        
        public void Initialize(RectTransform poolContainer, Action<FoodInBarView> onEmptyAction, Action<IPetFood> feedAction)
        {
            _poolContainer = poolContainer;
            _factory.Initialize(poolContainer, onEmptyAction, feedAction);
            CreatePool(START_POOL_SIZE);
        }

        public FoodInBarView GetFoodView()
        {
            if (_foodViewPool.Count == 0)
                _foodViewPool.Push(_factory.Create());

            return _foodViewPool.Pop();
        }

        public void ReturnToPool(FoodInBarView foodInBarView)
        {
            foodInBarView.transform.SetParent(_poolContainer);
            foodInBarView.gameObject.SetActive(false);
            _foodViewPool.Push(foodInBarView);
        }

        private void CreatePool(int amount)
        {
            _foodViewPool = new Stack<FoodInBarView>(amount);
            
            for (int i = 0; i < amount; i++)
            {
                _foodViewPool.Push(_factory.Create());
            }
        }
    }
}