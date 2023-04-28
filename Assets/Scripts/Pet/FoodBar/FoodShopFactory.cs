using Cysharp.Threading.Tasks;
using Services;
using Shop;
using UnityEngine;
using Utilities;
using Zenject;
using Object = UnityEngine.Object;

namespace Pet
{
    public class FoodShopFactory
    {
        private GameObject _foodInShopViewPref;

        private IPetFoodProvider _foodProvider;
        private IPlayerProgressService _progressService;
        private IShopController _shopController;

        private readonly IResourceLoader _resourceLoader;
        
        private const string FOOD_VIEW_KEY = "FoodInShopView";

        public FoodShopFactory()
        {
            _resourceLoader = new AddressableLoader();
        }

        [Inject]
        public void Construct(IPetFoodProvider foodProvider, IPlayerProgressService progressService, IShopController shopController)
        {
            _foodProvider = foodProvider;
            _progressService = progressService;
            _shopController = shopController;
        }
        
        public async UniTask Load()
        {
            _foodInShopViewPref = await _resourceLoader.Load<GameObject>(FOOD_VIEW_KEY);
        }
        
        public void Create(RectTransform container)
        {
            foreach (var food in _foodProvider.PetFood)
            {
                var createdObject = Object.Instantiate(_foodInShopViewPref, container);
                var foodView = createdObject.GetComponent<FoodInShopView>();
                foodView.Initialize(food, _progressService, _shopController);
            }
        }
    }
}