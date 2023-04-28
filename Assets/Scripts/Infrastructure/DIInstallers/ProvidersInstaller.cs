using LevelGenerator;
using Pet;
using Pipeline;
using Services;
using Zenject;

namespace Infrastructure
{
    public class ProvidersInstaller : MonoInstaller
    {
        public PageUI[] pages;
        public NotificationWindow notificationWindow;
        public ConfirmWindow confirmWindow;
        
        public override void InstallBindings()
        {
            BindPetFoodProvider();
            BindLevelsProvider();
            BindPagesProvider();
            BindPipelineLevelObjectProvider();
            BindMatchingCardsProvider();
        }

        private void BindMatchingCardsProvider()
        {
            CardsProvider cardsProvider = new CardsProvider();
            
            Container
                .Bind<ICardsProvider>()
                .FromInstance(cardsProvider)
                .AsSingle();
        }

        private void BindPipelineLevelObjectProvider()
        {
            PipelineObjectsProvider provider = new PipelineObjectsProvider();

            Container
                .Bind<IPipelineObjectsProvider>()
                .FromInstance(provider)
                .AsSingle();
        }

        private void BindPagesProvider()
        {
            PagesProvider pagesProvider = new PagesProvider(pages, confirmWindow, notificationWindow);

            Container
                .Bind<IPagesProvider>()
                .FromInstance(pagesProvider)
                .AsSingle();
        }

        private void BindPetFoodProvider()
        {
            PetFoodProvider foodProvider = new PetFoodProvider();

            Container
                .Bind<IPetFoodProvider>()
                .FromInstance(foodProvider)
                .AsSingle();
        }

        private void BindLevelsProvider()
        {
            LevelsProvider levelsProvider = new LevelsProvider();
            
            Container
                .Bind<ILevelsProvider>()
                .FromInstance(levelsProvider)
                .AsSingle();
        }
    }
}