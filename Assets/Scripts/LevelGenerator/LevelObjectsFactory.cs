using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Pipeline;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

namespace LevelGenerator
{
    public class LevelObjectsFactory
    {
        private readonly Dictionary<GameObjectTypes, GameObject> _prefabDictionary;

        private const string HAMSTER_HOUSE_KEY = "Prefab_HamsterHouse";
        private const string BOWL_KEY = "Prefab_Bowl";
        private const string PIPE_STRAIGHT_KEY = "Prefab_PipeStraight";
        private const string PIPE_STRAIGHT_CROSS_KEY = "Prefab_PipeStraightCross";
        private const string PIPE_CORNER_KEY = "Prefab_PipeCorner";
        private const string PIPE_CORNER_CROSS_KEY = "Prefab_PipeCornerCross";
        private const string CARD_KEY = "Prefab_MatchingCard";
        
        private readonly IResourceLoader _resourceLoader;
        private readonly RectTransform _objectContainer;
        
        public LevelObjectsFactory(RectTransform objectContainer)
        {
            _resourceLoader = new AddressableLoader();
            _prefabDictionary = new Dictionary<GameObjectTypes, GameObject>(7);
            _objectContainer = objectContainer;
        }
        
        public async UniTask LoadResources()
        {
            _prefabDictionary.Add(GameObjectTypes.House, await _resourceLoader.Load<GameObject>(HAMSTER_HOUSE_KEY));
            _prefabDictionary.Add(GameObjectTypes.Bowl, await _resourceLoader.Load<GameObject>(BOWL_KEY));
            _prefabDictionary.Add(GameObjectTypes.StraightPipe, await _resourceLoader.Load<GameObject>(PIPE_STRAIGHT_KEY));
            _prefabDictionary.Add(GameObjectTypes.StraightCrossPipe, await _resourceLoader.Load<GameObject>(PIPE_STRAIGHT_CROSS_KEY));
            _prefabDictionary.Add(GameObjectTypes.CornerPipe, await _resourceLoader.Load<GameObject>(PIPE_CORNER_KEY));
            _prefabDictionary.Add(GameObjectTypes.CornerCrossPipe, await _resourceLoader.Load<GameObject>(PIPE_CORNER_CROSS_KEY));
            _prefabDictionary.Add(GameObjectTypes.Card, await _resourceLoader.Load<GameObject>(CARD_KEY));
        }

        public T Create<T>(GameObjectTypes objectType)
        {
            GameObject instantiatedObject = Object.Instantiate(_prefabDictionary[objectType], _objectContainer);
            instantiatedObject.SetActive(false);
            
            return instantiatedObject.GetComponent<T>();
        }
    }
}