using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MatchingCards;
using Pipeline;
using UnityEngine;

namespace LevelGenerator
{
    public class LevelObjectsPool : ILevelObjectsPool
    {
        private readonly RectTransform _poolContainer;
        private readonly LevelObjectsFactory _levelObjectsFactory;
        private readonly Dictionary<GameObjectTypes, Stack<Pipe>> _pipesPool;
        private readonly Stack<Card> _cardsPool;

        private const int START_PIPE_POOL_SIZE = 5;
        private const int START_CARD_POOL_SIZE = 20;

        public LevelObjectsPool(RectTransform poolContainer)
        {
            _pipesPool = new Dictionary<GameObjectTypes, Stack<Pipe>>();
            _cardsPool = new Stack<Card>();
            _poolContainer = poolContainer;
            _levelObjectsFactory = new LevelObjectsFactory(_poolContainer);
        }

        public async UniTask Load()
        {
            await _levelObjectsFactory.LoadResources();
        }

        public void Initialize()
        {
            CreatePool();
        }

        public T GetLevelObject<T>(GameObjectTypes objectType)
        {
            return _levelObjectsFactory.Create<T>(objectType);
        }

        public Pipe GetPipe(GameObjectTypes objectType)
        {
            if (_pipesPool[objectType].Count == 0)
                CreateNewPipe(objectType);

            return _pipesPool[objectType].Pop();
        }

        public Card GetCard()
        {
            if(_cardsPool.Count == 0)
                _cardsPool.Push(_levelObjectsFactory.Create<Card>(GameObjectTypes.Card));
                
            return _cardsPool.Pop();
        }

        public void ReturnPipeToPool(Pipe levelObject)
        {
            levelObject.ReturnToPool(_poolContainer);
            
            var type = levelObject.ObjectConfig.gameObjectType;
            
            _pipesPool[type].Push(levelObject);
        }

        public void ReturnObjectToPool(GameObject levelObject)
        {
            levelObject.transform.SetParent(_poolContainer);
            levelObject.gameObject.SetActive(false);
        }

        public void ReturnCardToPool(Card card)
        {
            card.transform.SetParent(_poolContainer);
            card.gameObject.SetActive(false);
            _cardsPool.Push(card);
        }
        
        private void CreateNewPipe(GameObjectTypes objectType)
        {
            var newPipe = _levelObjectsFactory.Create<Pipe>(objectType);
            _pipesPool[objectType].Push(newPipe);
        }
        
        private void CreatePool()
        {
            FillCardPool(START_CARD_POOL_SIZE);
            FillPipePoolByType(GameObjectTypes.StraightPipe, START_PIPE_POOL_SIZE);
            FillPipePoolByType(GameObjectTypes.StraightCrossPipe, START_PIPE_POOL_SIZE);
            FillPipePoolByType(GameObjectTypes.CornerPipe, START_PIPE_POOL_SIZE);
            FillPipePoolByType(GameObjectTypes.CornerCrossPipe, START_PIPE_POOL_SIZE);
        }

        private void FillCardPool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _cardsPool.Push(_levelObjectsFactory.Create<Card>(GameObjectTypes.Card));
            }
        }

        private void FillPipePoolByType(GameObjectTypes objectType, int amount)
        {
            _pipesPool.Add(objectType, new Stack<Pipe>(amount));
            
            for (int i = 0; i < amount; i++)
            {
                _pipesPool[objectType].Push(_levelObjectsFactory.Create<Pipe>(objectType));
            }
        }
    }
}