using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Utilities;

namespace Pet
{
    public class FoodBar
    {
        private FoodBarView _barView;
        private readonly PetView _petView;
        private readonly FoodBarPool _pool;
        private readonly IPetFoodProvider _foodProvider;
        private readonly List<FoodInBarView> _displayedInBar;
        private readonly ICustomLogger _customLogger;

        public FoodBar(IPetFoodProvider petFoodProvider)
        {
            _foodProvider = petFoodProvider;
            _pool = new FoodBarPool();
            _displayedInBar = new List<FoodInBarView>();
            _customLogger = new DebugLogger();
        }

        public async UniTask Load()
        {
            await _pool.Load();
        }
        
        public void Initialize(Action<bool> onBarToggle, FoodBarView barView, Action<IPetFood> feedAction)
        {
            _barView = barView;
            _pool.Initialize(barView.FoodPoolContainer, RemoveFromBar, feedAction);
            _barView.Initialize(onBarToggle);
        }
        
        public void UpdateBar()
        {
            if (!_foodProvider.HasNonEmpty) return;
            
            _customLogger.PrintInfo("Food Bar", "Update bar");

            foreach (var food in _foodProvider.GetNonEmptyFood())
                AddToBar(food);
        }

        public void ClearBar()
        {
            if (_displayedInBar.Count == 0) return;

            _customLogger.PrintInfo("Food Bar", "Clear bar");

            foreach (var foodView in _displayedInBar)
            {
                foodView.CleanUp();
                _pool.ReturnToPool(foodView);
            }
            
            _displayedInBar.Clear();
        }
        
        private void AddToBar(IPetFood food)
        {
            var foodView = _pool.GetFoodView();
            SetupFoodView(food, foodView);
            
            _displayedInBar.Add(foodView);
        }

        private void RemoveFromBar(FoodInBarView foodView)
        {
            foodView.CleanUp();
            _displayedInBar.Remove(foodView);
            _pool.ReturnToPool(foodView);
        }

        private void SetupFoodView(IPetFood petFood, FoodInBarView foodView)
        {
            foodView.SetupView(petFood);
            foodView.transform.SetParent(_barView.FoodHolder);
            foodView.gameObject.SetActive(true);
        }
    }
}