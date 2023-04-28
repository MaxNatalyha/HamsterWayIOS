using LevelGenerator;
using MatchingCards;
using Pipeline;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GamesInstaller : MonoInstaller
    {
        public RectTransform poolContainer;
        public MatchingCardConfig matchingCardConfig;
        public PipelineGameLevelPage pipelineLevelPage;
        public MatchingCardsLevelPage matchingCardsLevelPage;

        public override void InstallBindings()
        {
            BindLevelObjectPool();

            BindPipelineLevelGenerator();
            BindMatchingLevelGenerator();
            
            BindPipelineGame();
            BindMatchingCardsGame();
        }

        private void BindLevelObjectPool()
        {
            LevelObjectsPool levelObjectsPool = new LevelObjectsPool(poolContainer);
            
            Container
                .Bind<ILevelObjectsPool>()
                .FromInstance(levelObjectsPool)
                .AsSingle();
        }

        private void BindPipelineGame()
        {
            PipelineGame pipelineGame = new PipelineGame(pipelineLevelPage);
        
            Container
                .Bind<IPipelineGame>()
                .FromInstance(pipelineGame)
                .AsSingle();
            
            Container.QueueForInject(pipelineGame);
        }        
        
        private void BindMatchingCardsGame()
        {
            MatchingCardsGame matchingCardsGame = new MatchingCardsGame(matchingCardsLevelPage);
            
            Container
                .Bind<IMatchingCardsGame>()
                .FromInstance(matchingCardsGame)
                .AsSingle();
            
            Container.QueueForInject(matchingCardsGame);
        }

        private void BindPipelineLevelGenerator()
        {
            PipelineLevelGenerator levelGenerator = new PipelineLevelGenerator();
            
            Container
                .Bind<IPipelineLevelGenerator>()
                .FromInstance(levelGenerator)
                .AsSingle();
            
            Container.QueueForInject(levelGenerator);
        }       
        
        private void BindMatchingLevelGenerator()
        {
            MatchingCardLevelGenerator levelGenerator = new MatchingCardLevelGenerator(matchingCardConfig);
            
            Container
                .Bind<IMatchingCardLevelGenerator>()
                .FromInstance(levelGenerator)
                .AsSingle();
            
            Container.QueueForInject(levelGenerator);
        }
    }
}