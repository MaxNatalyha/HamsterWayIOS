using System;
using System.Collections.Generic;
using GameUI;
using Pet;
using Services;
using Shop;
using Shop.SkinsSection;
using Zenject;

namespace Infrastructure
{
    public class MainInstaller : MonoInstaller
    {
        public ShopPage shopPage;
        public MainPage mainPage;
        public LoadingPage loadingPage;
        
        public PetConfig petConfig;
        public PetFoodConfigurations petFoodConfig;
        public Updater updater;

        public override void InstallBindings()
        {
            BindLoadingPage();
            BindShopController();
            BindPetController();
            BindUIFactory();
            BindPetFoodFactory();
            BindGlobalStateMachine();
        }

        private void BindPetFoodFactory()
        {
            PetFoodFactory foodFactory = new PetFoodFactory(petFoodConfig);

            Container.BindInstance(foodFactory);
        }

        private void BindPetController()
        {
            PetController petController = new PetController(petConfig, updater, mainPage);
            
            Container
                .Bind<IPetController>()
                .FromInstance(petController)
                .AsSingle();
            
            Container.QueueForInject(petController);
        }

        private void BindShopController()
        {
            SkinShowcase skinShowcase = new SkinShowcase(shopPage.skinShowcaseView);
            
            Container.QueueForInject(skinShowcase);
            
            ShopController shopController = new ShopController(shopPage, skinShowcase);
        
            Container
                .Bind<IShopController>()
                .FromInstance(shopController)
                .AsSingle();
            
            Container.QueueForInject(shopController);
        }

        private void BindLoadingPage()
        {
            Container
                .Bind<ILoadingPage>()
                .FromInstance(loadingPage)
                .AsSingle();
        }

        private void BindUIFactory()
        {
            PipelineLevelsButtonsFactory pipelineFactory = new PipelineLevelsButtonsFactory();
            MatchingCardsLevelsButtonsFactory matchingCardsFactory = new MatchingCardsLevelsButtonsFactory();
            FoodShopFactory foodShopFactory = new FoodShopFactory();
            
            Container.QueueForInject(pipelineFactory);
            Container.QueueForInject(matchingCardsFactory);
            Container.QueueForInject(foodShopFactory);

            UIBuilder uiBuilder = new UIBuilder(pipelineFactory, matchingCardsFactory, foodShopFactory);
            
            Container
                .Bind<IUIBuilder>()
                .FromInstance(uiBuilder)
                .AsSingle();
            
            Container.QueueForInject(uiBuilder);
        }

        private void BindGlobalStateMachine()
        {
            GlobalStateMachine stateMachine = new GlobalStateMachine();

            var loadingState = new LoadingState(stateMachine);
            var initializeState = new InitializeState(stateMachine);
            var buildUIState = new BuildUIState(stateMachine);
            var gameState = new GameState();
            var quitState = new QuitState();
            
            var states = new Dictionary<Type, IGlobalState>
            {
                [typeof(LoadingState)] = loadingState,
                [typeof(InitializeState)] = initializeState,
                [typeof(BuildUIState)] = buildUIState,
                [typeof(GameState)] = gameState,
                [typeof(QuitState)] = quitState
            };

            stateMachine.SetStates(states);
            
            Container.QueueForInject(loadingState);
            Container.QueueForInject(initializeState);
            Container.QueueForInject(buildUIState);
            Container.QueueForInject(gameState);
            Container.QueueForInject(quitState);
            
            Container.BindInstance(stateMachine).AsSingle();
        }
    }
}