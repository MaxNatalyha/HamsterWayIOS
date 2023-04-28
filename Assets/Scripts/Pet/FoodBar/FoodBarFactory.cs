using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

namespace Pet
{
    public class FoodBarFactory
    {
        private GameObject _foodInBarViewPref;
        
        private RectTransform _container;
        private Action<FoodInBarView> _onEmptyAction;
        private Action<IPetFood> _feedAction;
        
        private readonly IResourceLoader _resourceLoader;

        private const string FOOD_VIEW_KEY = "FoodInBarView";

        public FoodBarFactory()
        {
            _resourceLoader = new AddressableLoader();
        }

        public async UniTask Load()
        {
            _foodInBarViewPref = await _resourceLoader.Load<GameObject>(FOOD_VIEW_KEY);
        }

        public void Initialize(RectTransform container, Action<FoodInBarView> onEmptyAction, Action<IPetFood> feedAction)
        {
            _container = container;
            _onEmptyAction = onEmptyAction;
            _feedAction = feedAction;
        }

        public FoodInBarView Create()
        {
            var createdObject = Object.Instantiate(_foodInBarViewPref, _container);
            createdObject.gameObject.SetActive(false);
            
            var foodView = createdObject.GetComponent<FoodInBarView>();
            foodView.Initialize(_onEmptyAction, _feedAction);
            
            return foodView;
        }
    }
}