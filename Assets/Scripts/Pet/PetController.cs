using System;
using Cysharp.Threading.Tasks;
using GameUI;
using Services;
using UnityEngine;
using Utilities;
using Zenject;

namespace Pet
{
    public class PetController : IPetController, IUpdatable
    {
        public event Action<float> OnSatietyLevelChangeEvent;

        public float Satiety
        {
            get => _satiety;
            private set
            {
                _satiety = value;
                OnSatietyChange();
            }
        }
        
        private float _satiety;
        private float _timer;

        private FoodBar _foodBar;
        private IPetFoodProvider _foodProvider;

        private readonly MainPage _mainPage;
        private readonly PetConfig _petConfig;
        private readonly PetStateMachine _petViewStateMachine;
        private readonly ISaver _saver;
        private readonly IUpdater _updater;
        private readonly ICustomLogger _logger;

        private const string LAST_MEAL_KEY = "LastMeal";
        private const string SATIETY_KEY = "Satiety";
        
        public PetController(PetConfig petConfig, IUpdater updater, MainPage mainPage)
        {
            _petConfig = petConfig;
            _mainPage = mainPage;
            _updater = updater;
            
            _petViewStateMachine = new PetStateMachine();
            _saver = new PrefsSaver();
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IPetFoodProvider foodProvider)
        {
            _foodProvider = foodProvider;
            _foodBar = new FoodBar(_foodProvider);
        }

        public async UniTask Load()
        {
            await _foodBar.Load();
            LoadSatiety();
        }

        public void Initialize()
        {
            _foodBar.Initialize(OnBarToggle, _mainPage.foodBarView, Feed);
            _petViewStateMachine.Initialize(_petConfig.petStates, _mainPage.petView);
            _mainPage.petView.Initialize(onEnable: () => _petViewStateMachine.UpdateState(_satiety));
            
            _mainPage.foodPacketAdsButton.SetRewardAdCompleteAction(OpenFoodPacket);
            
            _updater.Add(this);
        }

        public void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _petConfig.tickInSeconds)
            {
                Satiety = Mathf.Clamp(Satiety - _petConfig.satietySpendPerTick, _petConfig.minSatiety , _petConfig.maxSatiety);
                _timer = 0;
                _logger.PrintInfo("Pet", $"-1 Satiety. Current - {_satiety}");
            }
        }

        public void Save()
        {
            _saver.SetFloat(SATIETY_KEY, _satiety);
            _saver.SetString(LAST_MEAL_KEY, DateTime.Now.ToBinary().ToString());
            _logger.PrintInfo("Pet Controller", $"Save satiety - {_satiety}");
        }

        private void OnSatietyChange()
        {
            _petViewStateMachine.UpdateState(_satiety);
            OnSatietyLevelChangeEvent?.Invoke(_satiety);
        }
        
        private void OpenFoodPacket()
        {
            _mainPage.foodPacketAdsButton.gameObject.SetActive(false);
            
            var randomFood = _foodProvider.GetRandomFood();
            
            _mainPage.foodPacketView.SetFoodIcons(randomFood);
            _mainPage.foodPacketView.Show();
            
            foreach (var food in randomFood)
                food.State.Amount++;
        }
        
        private void OnBarToggle(bool value)
        {
            if (value)
            {
                _foodBar.UpdateBar();
                UpdatePetView();
            }           
            else
            {
                _foodBar.ClearBar();
                _mainPage.petView.OnBarClose();
            }
        }
        
        private void UpdatePetView()
        {
            if(_foodProvider.HasNonEmpty)
                _mainPage.petView.PlayHungryAnimation(_foodProvider.GetMostSatietyFood().Config.FoodSprite);
            else
                _mainPage.petView.PlayWaitingAnimation();
        }

        private void Feed(IPetFood food)
        {
            _mainPage.petView.PlayEatingAnimation(food);

            food.State.Amount--;
            Satiety = Mathf.Clamp(Satiety + food.Config.Satiety, _petConfig.minSatiety, _petConfig.maxSatiety);

            _logger.PrintInfo("Pet", $"Feed by {food.Config.FoodType}, with {food.Config.Satiety} satiety");
            _saver.SetString(LAST_MEAL_KEY, DateTime.Now.ToBinary().ToString());
        }

        private void LoadSatiety()
        {
            _satiety = _saver.HasKey(SATIETY_KEY) ? _saver.GetFloat(SATIETY_KEY) : _petConfig.maxSatiety;
            _logger.PrintInfo("Pet Controller", $"Load satiety - {_satiety}");

            if (_saver.HasKey(LAST_MEAL_KEY))
                CheckSinceLastMeal();
        }

        private void CheckSinceLastMeal()
        {
            long temp = Convert.ToInt64(_saver.GetString(LAST_MEAL_KEY));
            
            DateTime lastMealDateTime = DateTime.FromBinary(temp);
            TimeSpan difference = DateTime.Now.Subtract(lastMealDateTime);

            var differenceTotalMinutes = (int)difference.TotalMinutes;
            var totalSatietyLeft = differenceTotalMinutes / 15; // 1 satiety per 15 min

            _satiety = Mathf.Clamp(_satiety - totalSatietyLeft, _petConfig.minSatiety, _petConfig.maxSatiety);
            _logger.PrintInfo("Pet Controller", $"Minutes since last pet meal - {differenceTotalMinutes}; Satiety left - {totalSatietyLeft}; Current satiety - {_satiety}");
        }
    }
}
