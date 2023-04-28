using Pet;
using SaveSystem;
using Services;
using UnityEngine;
using Utilities;
using Zenject;

namespace Infrastructure
{
    public class QuitState : IGlobalState
    {
        private ISkinsService _skinsService;
        private IPetController _petController;
        private IPetFoodProvider _petFoodProvider;
        private IPlayerProgressService _progressService;
        
        private readonly ICustomLogger _logger;
        
        public QuitState()
        {
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(IPetFoodProvider petFoodProvider, IPetController petController, IPlayerProgressService progressService, ISkinsService skinsService)
        {
            _skinsService = skinsService;
            _petController = petController;
            _petFoodProvider = petFoodProvider;
            _progressService = progressService;
        }

        public void Enter()
        {
            _logger.PrintInfo("Global Quit State", "Enter");

            SavePlayerData();
            SaveSkinsData();
            SavePet();
            SavePetFood();

            PlayerPrefs.Save();
        }

        private void SavePetFood()
        {
            foreach (var food in _petFoodProvider.PetFood)
                food.Save();
        }

        private void SavePet()
        {
            _petController.Save();
        }

        private void SavePlayerData()
        {
            var playerDataLoader = new PlayerDataLoader();
            playerDataLoader.Save(_progressService.GetPlayerData());
        }

        private void SaveSkinsData()
        {
            var skinsDataLoader = new SkinsDataLoader();
            skinsDataLoader.Save(_skinsService.Save());
        }
    }
}